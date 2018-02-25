using System;
using System.Linq;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Extensions;

namespace KTaNE_Helper.Modules.Modded
{
    public class Neutralization
    {
        private readonly Acid _acidType;
        private Base _baseType;
        private readonly int _acidVol;
        private int _baseVol;
        private bool _soluType;
        private readonly KMBombInfo _info = new KMBombInfo();
        private readonly bool _invalid;

        private readonly string[] _acidForm = { "HF", "HCl", "HBr", "HI" };
	    private readonly string[] _baseForm = { "NH3", "LiOH", "NaOH", "KOH" };
	    private string[] _dispForm = { "NH\u2083", "LiOH", "NaOH", "KOH" }, _clrName = { "Yellow", "Green", "Red", "Blue" };

	    private readonly bool[,] _solubility = {
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
            _acidType = (Acid)acid;
            _acidVol = (volume+1)*5;

            if (volume < 0 || acid < 0 || volume > 3 || acid > 3)
                _invalid = true;

            PrepareBase();
            PrepareConc();
        }

        public string GetAnswer()
        {
            return !_invalid ? $@"base {_baseType}{Environment.NewLine}conc set {_baseVol}{Environment.NewLine}{(_soluType ? "filter" + Environment.NewLine : "")}titrate" : "";
        }

	    private void PrepareBase()
        {
            string temp = string.Join("", _info.GetIndicators().ToArray());

            Debug.LogFormat("[Neutralization #{0}] >Report B: Base preparation", _moduleId);

            if (_info.IsIndicatorPresent(KMBombInfoExtensions.KnownIndicatorLabel.NSA) && _info.GetBatteryCount() == 3)
            {
                _baseType = Base.Nh3;
            }
            else if (_info.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.CAR) || 
                _info.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.FRQ) || 
                _info.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.IND))
            {
                _baseType = Base.Koh;
                Debug.LogFormat("[Neutralization #{0}] B:\\>CAR, FRQ, or IND: {1}", _moduleId, _baseForm[(int)_baseType]);
            }
            else if (_info.GetPortCount() == 0 && _info.GetSerialNumberLetters().Any("AEIOU".Contains))
            {
                _baseType = Base.LiOh;
                Debug.LogFormat("[Neutralization #{0}] B:\\>No ports and vowel in S/N: {1}", _moduleId, _baseForm[(int)_baseType]);
            }
            else if (temp.Any(_acidForm[(int)_acidType].ToUpper().Contains))
            {
                _baseType = Base.Koh;
                Debug.LogFormat("[Neutralization #{0}] B:\\>Acid formular has letter in common with an indicator: {1}", _moduleId, _baseForm[(int)_baseType]);
            }
            else if (_info.GetBatteryCount(KMBombInfoExtensions.KnownBatteryType.D) > _info.GetBatteryCount(KMBombInfoExtensions.KnownBatteryType.AA))
            {
                _baseType = Base.Nh3;
                Debug.LogFormat("[Neutralization #{0}] B:\\>D batt > AA batt: {1}", _moduleId, _baseForm[(int)_baseType]);
            }
            else if (_acidType == Acid.Hf || _acidType == Acid.HCl)
            {
                _baseType = Base.NaOh;
                Debug.LogFormat("[Neutralization #{0}] B:\\>Anion < 20: {1}", _moduleId, _baseForm[(int)_baseType]);
            }
            else
            {
                _baseType = Base.LiOh;
                Debug.LogFormat("[Neutralization #{0}] B:\\>Otherwise: {1}", _moduleId, _baseForm[(int)_baseType]);
            }
        }

	    private void PrepareConc()
        {
            int[] anion = { 9, 17, 35, 53 }, cation = { 1, 3, 11, 19 }, len = { 1, 2, 2, 1 };
            int bh = _info.GetBatteryHolderCount(), port = _info.GetPorts().Distinct().Count(), indc = _info.GetIndicators().Count();

	        int baseConc;

            Debug.LogFormat("[Neutralization #{0}] >Report C: Concentration calculation", _moduleId);

            //acid
            int acidConc = anion[(int)_acidType];
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Starts with anion: {1} [current = {2}]", _moduleId, anion[(int)_acidType], acidConc);
            acidConc -= cation[(int)_baseType];
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Sub with cation: -{1} [current = {2}]", _moduleId, cation[(int)_baseType], acidConc);
            if (_acidType == Acid.Hi || _baseType == Base.LiOh || _baseType == Base.NaOh)
            {
                acidConc -= 4;
                Debug.LogFormat("[Neutralization #{0}] C:\\acid>Anion/Cation contains vowels: -4 [current = {1}]", _moduleId, acidConc);
            }
            if (len[(int)_acidType] == len[(int)_baseType])
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
                acidConc = (_acidVol * 2) / 5;
                Debug.LogFormat("[Neutralization #{0}] C:\\acid>LSD is 0, use (Acid vol x2)/5: {1}", _moduleId, acidConc);
            }
            Debug.LogFormat("[Neutralization #{0}] C:\\acid>Final acid conc = {1}", _moduleId, acidConc / 10f);

            //base
            if ((_acidType == Acid.Hi && _baseType == Base.Koh) || (_acidType == Acid.HCl && _baseType == Base.Nh3))
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
            else if (_baseType == Base.NaOh)
            {
                baseConc = 10;
                Debug.LogFormat("[Neutralization #{0}] C:\\base>Tie, use 10 because it's closest to 11", _moduleId);
            }
            else if (_baseType == Base.Koh)
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
            _baseVol = 20 / baseConc;
            Debug.LogFormat("[Neutralization #{0}] C:\\drop>Starts with 20 / Base conc. {1} = {2}", _moduleId, baseConc, _baseVol);
            _baseVol *= (_acidVol * acidConc) / 10;
            Debug.LogFormat("[Neutralization #{0}] C:\\drop>Then mult with Acid vol {1} and Acid conc. {2} = {3}", _moduleId, _acidVol, acidConc / 10f, _baseVol);
            Debug.LogFormat("[Neutralization #{0}] C:\\drop>Final drop count is {1}", _moduleId, _baseVol);
            _soluType = _solubility[(int)_acidType, (int)_baseType];
	        Debug.LogFormat(_soluType 
				? "[Neutralization #{0}] C:\\solu>Pair of {1} and {2} is not soluble, turn filter on." 
				: "[Neutralization #{0}] C:\\solu>Pair of {1} and {2} is soluble, turn filter off.", _moduleId, _acidForm[(int) _acidType], _baseForm[(int) _baseType]);
        }


    }

    public enum Acid
    {
        Hf,
        HCl,
        HBr,
        Hi
    }

    public enum Base
    {
        Nh3,
        LiOh,
        NaOh,
        Koh
    }
}