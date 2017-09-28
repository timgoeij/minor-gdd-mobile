using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    public bool laserColorChanges {
        get;
        private set;
    }

    public bool boxColorChanges {
        get;
        private set;
    }

	private List<PatternToAdd> _patternsToAdd;

	// Use this for initialization
	void Start () {
        _patternsToAdd = new List<PatternToAdd> {

            new PatternToAdd
            {
                Pattern = new FirstUpAndDown(),
                AddAfterSeconds = 20,
                Notification = "?! My God, Why @re they m0ving ?!",
                ExtraPatterns = new List<LevelPattern>()
                {
                    new UpAndDownLaserPattern(),
                    new UpDownUpDown(),
                    new UpDownUpDownFast(),
                    new MixedUpDown()
                },
            },

            new PatternToAdd
            {
                Pattern = new FirstRotators(),
                AddAfterSeconds = 40,
                Notification = "#$# Welcome to Windmill Park! #%#",
                ExtraPatterns = new List<LevelPattern>() {
                    new Windmillpark(),
                    new SmallPark(),
                    new QuickMix(),
                    new NiceMix()
                }
            },

            new PatternToAdd
            {
                Pattern = new FirstBox(),
                AddAfterSeconds = 60,
                Notification = ">:( Don't get trapped...",
                ExtraPatterns = new List<LevelPattern>()
                {
                    new RotationBoxPattern(),
                    new UpAndDownLaserBox(),
                    new QuickUpAndDownBox()
                }
            },

            new PatternToAdd
            {
                Pattern = new FirstChangeableUpAndDownLaser(),
                AddAfterSeconds = 80,
                Notification = "^% $ Trust Nob0dy $ %^",
                ExtraPatterns = new List<LevelPattern>(),
            },

            new PatternToAdd
            {
                Pattern = new FirstCircle(),
                AddAfterSeconds = 100,
                Notification = "!!!!!! I N C O M I NG !!!!!!",
                ExtraPatterns = new List<LevelPattern>()
                {
                    new Circles(),
                    new DownhillCircles()
                }
            },

            new PatternToAdd
            {
                Pattern = new FirstChangeableBox(),
                AddAfterSeconds = 120,
                Notification = "#MoreDangerous",
                ExtraPatterns = new List<LevelPattern>()
                {
                    new UpAndDownChangeableLaserBox()
                }
            },

            new PatternToAdd
            {
                Pattern = new FirstTriangles(),
                AddAfterSeconds = 160,
                Notification = "#$# SHARKS ?!? #%#",
                ExtraPatterns = new List<LevelPattern>()
                {
                    new DownHillTriangles(),
                    new TrianglesAndCircles()
                }
            },

            new PatternToAdd
            {
                Pattern = new FirstFallingPattern(),
                AddAfterSeconds = 180,
                Notification = "l3arNiNg h0w t0 f4lL",
                ExtraPatterns = new List<LevelPattern>(){
                    new LaserFallingPattern(),
                    new CubeFallingPattern(),
                    new SharkFallingPattern()
                }
            },

            new PatternToAdd
            {
                Pattern = new FirstMeteorStorm(),
                AddAfterSeconds = 200,
                Notification = "^^ tiME To takE a shOweR! ^^",
                ExtraPatterns = new List<LevelPattern>()
                {
                    new DoubleMeteorStorm(),
                    new SmallMeteorStorm(),
                    new BigMeteorStorm(),
                    new MeteorValley()
                },
            },

            new PatternToAdd
            {
                Pattern = new NewColorPattern(),
                AddAfterSeconds = 220,
                Notification = ":D NEW COLOR  - DEATH IMMINENT :D",
                ExtraPatterns = new List<LevelPattern>(){},
                Color = Color.magenta
            }
        };
	}

	public bool HasPatternToAdd() {
		return (GetPatternToAdd() != null);
	}

	public PatternToAdd GetPatternToAdd() {
		return _patternsToAdd.Where(
			p => p.AddAfterSeconds < Timer.TotalTime && 
					 p.Added == false
		).FirstOrDefault();
	}

    public void AllowLaserColorChanges() {
        laserColorChanges = true;
    }

    public void AllowBoxColorChanges() {
        boxColorChanges = true;
    }
}

public class PatternToAdd {
	public float AddAfterSeconds;
	public LevelPattern Pattern;
	public bool Added = false;
	public List<LevelPattern> ExtraPatterns = new List<LevelPattern>();
    public string Notification = "";
    public Color? Color = null;
    public delegate void OnCall ();
}
