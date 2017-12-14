using System;
using System.Linq;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Extensions;

namespace KTaNE_Helper.Modules.Modded
{
    public class Neutralization
    {
        private Acid acidType;
        private Base baseType;
        private int acidVol;
        private int baseVol;
        private bool soluType;
        private KMBombInfo Info = new KMBombInfo();
        private bool Invalid;

        private string[] _acidForm = { "HF", "HCl", "HBr", "HI" }, _baseForm = { "NH3", "LiOH", "NaOH", "KOH" }, _dispForm = { "NH\u2083", "LiOH", "NaOH", "KOH" }, clrName = { "Yellow", "Green", "Red", "Blue" };
        private bool[,] solubility = new bool[4, 4] {
        {true,true,false,false},
        {true,false,true,true},
        {false,true,false,true},
        {false,false,true,false},
    };

        private static int _moduleCounter = 1;
        private readonly int _moduleId;

        public Neutralization(int acid, int volume)
        {
            _moduleId = _moduleCounter;
            _moduleCounter++;
            acidType = (Acid)acid;
            acidVol = (volume+1)*5;

            if (volume < 0 || acid < 0 || volume > 3 || acid > 3)
                Invalid = true;

            prepareBase();
            prepareConc();
        }

        public string GetAnswer()
        {
            return !Invalid ? $@"base {baseType}{Environment.NewLine}conc set {baseVol}{Environment.NewLine}{(soluType ? "filter" + Environment.NewLine : "")}titrate" : "";
        }

        void prepareBase()
        {
            string temp = string.Join("", Info.GetIndicators().ToArray());

            Debug.LogFormat("[Neutralization #{0}] >Report B: Base preparation", _moduleId);

            if (Info.IsIndicatorPresent(KMBombInfoExtensions.KnownIndicatorLabel.NSA) && Info.GetBatteryCount() == 3)
            {
                baseType = Base.NH3;
            }
            else if (Info.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.CAR) || 
                Info.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.FRQ) || 
                Info.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.IND))
            {
                baseType = Base.KOH;
                Debug.LogFormat("[Neutralization #{0}] B:\\>CAR, FRQ, or IND: {1}", _moduleId, _baseForm[(int)baseType]);
            }
            else if (Info.GetPortCount() == 0 && Info.GetSerialNumberLetters().Any("AEIOU".Contains))
            {
                baseType = Base.LiOH;
                Debug.LogFormat("[Neutralization #{0}] B:\\>No ports and vowel in S/N: {1}", _moduleId, _baseForm[(int)baseType]);
            }
            else if (temp.Any(_acidForm[(int)acidType].ToUpper().Contains))
            {
                baseType = Base.KOH;
                Debug.LogFormat("[Neutralization #{0}] B:\\>Acid formular has letter in common with an indicator: {1}", _moduleId, _baseForm[(int)baseType]);
            }
            else if (Info.GetBatteryCount(KMBombInfoExtensions.KnownBatteryType.D) > Info.GetBatteryCount(KMBombInfoExtensions.KnownBatteryType.AA))
            {
                baseType = Base.NH3;
                Debug.LogFormat("[Neutralization #{0}] B:\\>D batt > AA batt: {1}", _moduleId, _baseForm[(int)baseType]);
            }
            else if (acidType == Acid.HF || acidType == Acid.HCl)
            {
                baseType = Base.NaOH;
                Debug.LogFormat("[Neutralization #{0}] B:\\>Anion < 20: {1}", _moduleId, _baseForm[(int)baseType]);
            }
            else
            {
                baseType = Base.LiOH;
                Debug.LogFormat("[Neutralization #{0}] B:\\>Otherwise: {1}", _moduleId, _baseForm[(int)baseType]);
            }
        }

        void prepareConc()
        {
            int[] anion = { 9, 17, 35, 53 }, cation = { 1, 3, 11, 19 }, len = { 1, 2, 2, 1 };
            int bh = Info.GetBatteryHolderCount(), port = Info.GetPorts().Distinct().Count(), indc = Info.GetIndicators().Count();

            int acidConc;
            int baseConc;

            Debug.LogFormat("[Neutralization #{0}] >Report C: Concentration calculation", _moduleId);

            //acid
            acidConc = anion[(int)acidType];
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Starts with anion: {1} [current = {2}]", _moduleId, anion[(int)acidType], acidConc);
            acidConc -= cation[(int)baseType];
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Sub with cation: -{1} [current = {2}]", _moduleId, cation[(int)baseType], acidConc);
            if (acidType == Acid.HI || baseType == Base.LiOH || baseType == Base.NaOH)
            {
                acidConc -= 4;
                Debug.LogFormat("[Neutralization #{0}] C:\\acid>Anion/Cation contains vowels: -4 [current = {1}]", _moduleId, acidConc);
            }
            if (len[(int)acidType] == len[(int)baseType])
            {
                acidConc *= 3;
                Debug.LogFormat("[Neutralization #{0}] C:\\acid>Length of anion = cation: x3 [current = {1}]", _moduleId, acidConc);
            }
            if (acidConc <= 0)
            {
                acidConc *= -1;
                Debug.LogFormat("[Neutralization #{0}] C:\\acid>Negative adjust [current = {1}]", _moduleId, acidConc);
            }
            acidConc %= 10;
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Take LSD: {1}", _moduleId, acidConc);
            if (acidConc == 0)
            {
                acidConc = (acidVol * 2) / 5;
                Debug.LogFormat("[Neutralization #{0}] C:\\acid>LSD is 0, use (Acid vol x2)/5: {1}", _moduleId, acidConc);
            }
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Final acid conc = {1}", _moduleId, acidConc / 10f);

            //base
            if ((acidType == Acid.HI && baseType == Base.KOH) || (acidType == Acid.HCl && baseType == Base.NH3))
            {
                baseConc = 20;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Special pair, use constant conc. 20", _moduleId);
            }
            else if (bh > port && bh > indc)
            {
                baseConc = 5;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Battery holders win, use conc. 5", _moduleId);
            }
            else if (port > bh && port > indc)
            {
                baseConc = 10;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Port types win, use conc. 10", _moduleId);
            }
            else if (indc > bh && indc > port)
            {
                baseConc = 20;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Indicators win, use conc. 20", _moduleId);
            }
            else if (baseType == Base.NaOH)
            {
                baseConc = 10;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Tie, use 10 because it's closest to 11", _moduleId);
            }
            else if (baseType == Base.KOH)
            {
                baseConc = 20;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Tie, use 20 because it's closest to 19", _moduleId);
            }
            else
            {
                baseConc = 5;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Tie, use 5 because it's closest to 1 or 3", _moduleId);
            }

            //drop cnt & solu
            baseVol = 20 / baseConc;
            Debug.LogFormat("[Neutralization #{0}] C:\\drop>Starts with 20 / Base conc. {1} = {2}", _moduleId, baseConc, baseVol);
            baseVol *= (acidVol * acidConc) / 10;
            Debug.LogFormat("[Neutralization #{0}] C:\\drop>Then mult with Acid vol {1} and Acid conc. {2} = {3}", _moduleId, acidVol, acidConc / 10f, baseVol);
            Debug.LogFormat("[Neutralization #{0}] C:\\drop>Final drop count is {1}", _moduleId, baseVol);
            soluType = solubility[(int)acidType, (int)baseType];
            if (soluType == true) Debug.LogFormat("[Neutralization #{0}] C:\\solu>Pair of {1} and {2} is not soluble, turn filter on.", _moduleId, _acidForm[(int)acidType], _baseForm[(int)baseType]);
            else Debug.LogFormat("[Neutralization #{0}] C:\\solu>Pair of {1} and {2} is soluble, turn filter off.", _moduleId, _acidForm[(int)acidType], _baseForm[(int)baseType]);
        }


    }

    public enum Acid
    {
        HF,
        HCl,
        HBr,
        HI
    }

    public enum Base
    {
        NH3,
        LiOH,
        NaOH,
        KOH
    }
}