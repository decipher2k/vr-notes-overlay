﻿using System.Drawing;
using UnityEngine;
using Valve.VR;

namespace EVRC
{
    using Utils = OverlayUtils;

    public class ControllerOverlay : MonoBehaviour
    {
        public Texture texture;
        public string overlayKey;
        private ulong handle = OpenVR.k_ulOverlayHandleInvalid;
        private Texture lastTexture;

        public string key
        {
            get
            {
                return Utils.GetKey(overlayKey);
            }
        }

        void Init()
        {
            Utils.CreateOverlay(key, gameObject.name, ref handle);
        }

        void Update()
        {
            var overlay = OpenVR.Overlay;
            if (overlay == null) return;

            if (handle == OpenVR.k_ulOverlayHandleInvalid)
            {
                Init();
            }
            string fontName = "Cambria";
            System.Drawing.Font testFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 400.0f, System.Drawing.FontStyle.Regular,
                                     GraphicsUnit.Pixel);
            texture = new Texture2D(2,2 );
            if (ButtonHandler.note != GameObject.Find("txtNote").GetComponent<TMPro.TextMeshProUGUI>().text)
            {
                ((Texture2D)texture).LoadImage(TxtToImage.DrawText(ButtonHandler.note, testFont, System.Drawing.Color.Orange, 3000, ".\\"));

                if (texture != null && handle != OpenVR.k_ulOverlayHandleInvalid)
                {
                    var o = new Utils.OverlayHelper(handle);
                    o.Show();

                    if (texture != lastTexture)
                    {
                        o.SetFullTexture(texture);
                        lastTexture = texture;
                    }
                    o.SetColorWithAlpha(UnityEngine.Color.white);
                    o.SetWidthInMeters(.05f);



                    o.SetInputMethod(VROverlayInputMethod.None);
                    o.SetMouseScale(1, 1);

                    var offset = new SteamVR_Utils.RigidTransform(transform);
                    if (!Utils.IsFacingHmd(transform))
                    {
                        offset.rot = offset.rot * Quaternion.AngleAxis(180, Vector3.up);
                    }

                    o.SetTransformAbsolute(ETrackingUniverseOrigin.TrackingUniverseStanding, offset);
                }
            }
        }

        void OnDisable()
        {
            var o = new Utils.OverlayHelper(handle, false);
            if (o.Valid)
            {
                o.Destroy();
            }

            handle = OpenVR.k_ulOverlayHandleInvalid;
        }
    }
}
