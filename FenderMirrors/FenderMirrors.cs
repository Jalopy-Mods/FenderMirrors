using JaLoader;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using UnityEngine;

namespace FenderMirrors
{
    public class FenderMirrors : Mod
    {
        public override string ModID => "FenderMirrors";
        public override string ModName => "Fender Mirrors";
        public override string ModAuthor => "Leaxx";
        public override string ModDescription => "Adds a buyable upgrade that moves the door side mirror to be a fender mirror.";
        public override string ModVersion => "1.0";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/FenderMirrors";
        public override string NexusModsLink => "";
        public override WhenToInit WhenToInit => WhenToInit.InGame;
        public override List<(string, string, string)> Dependencies => new List<(string, string, string)>()
        {
            ("JaLoader", "Leaxx", "4.0.3")
        };
        public override List<(string, string, string)> Incompatibilities => new List<(string, string, string)>()
        {
        };

        public override bool UseAssets => false;

        public override void CustomObjectsRegistration()
        {
            base.CustomObjectsRegistration();

            ModHelper.Instance.SetExtraToUseDefaultIcon("fenderMirrors");

            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateCustomExtraObject(
                BoxSizes.Medium,
                "Fender Mirrors",
                "Moves the door side mirror to be a fender mirror.",
                15,
                1,
                "fenderMirrors",
                AttachExtraTo.Body,
                this), "fenderMirrors");
        }

        public override void OnExtraAttached(string extraName)
        {
            base.OnExtraAttached(extraName);

            if(extraName == "fenderMirrors")
            {
                Transform mirror;
                var frame = GameObject.Find("FrameHolder").transform;
                frame = frame.Find("TweenHolder/Frame");

                try
                {
                    mirror = frame.Find("L_Door/WingMirror");
                }
                catch (System.Exception)
                {
                    // the extra was already installed
                    return;
                }

                mirror.SetParent(frame, true);

                mirror.localPosition = new Vector3(5.80f, -1.5f, -2.7f);
                mirror.localEulerAngles = new Vector3(0f, 281.6f, 162.9f);
                Destroy(mirror.GetComponentInChildren<MirrorLogicC>());
                Destroy(mirror.GetComponentInChildren<BoxCollider>());
            }
        }
    }
}
