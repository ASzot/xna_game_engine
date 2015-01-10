#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace RenderingSystem
{
    public class DebugTimings
    {
        private bool b_update = true;
        // Rendering
        private Stopwatch sw_shadowRender = new Stopwatch();
        private Stopwatch sw_gbufferRender = new Stopwatch();
        private Stopwatch sw_lightRender = new Stopwatch();
        private Stopwatch sw_reconstructShadingRender = new Stopwatch();
        private Stopwatch sw_ssaoRender = new Stopwatch();
        private Stopwatch sw_postProcessRender = new Stopwatch();
        private Stopwatch sw_totalRender = new Stopwatch();

        private double d_shadowRenderMs;
        private double d_gbufferRenderMs;
        private double d_lightRenderMs;
        private double d_reconstructShadingRenderMs;
        private double d_ssaoRenderMs;
        private double d_postProccessRenderMs;

        // Updating.
        private Stopwatch sw_totalUpdate = new Stopwatch();
        private Stopwatch sw_camUpdate = new Stopwatch();
        private Stopwatch sw_rendererUpdate = new Stopwatch();
        private Stopwatch sw_lightUpdate = new Stopwatch();
        private Stopwatch sw_objUpdate = new Stopwatch();
        private Stopwatch sw_playerUpdate = new Stopwatch();
        private Stopwatch sw_procUpdate = new Stopwatch();
        private Stopwatch sw_levelUpdate = new Stopwatch();
        private Stopwatch sw_particleUpdate = new Stopwatch();
        private Stopwatch sw_formUpdate = new Stopwatch();
        private Stopwatch sw_syncUpdate = new Stopwatch();
        private Stopwatch sw_ai1Update = new Stopwatch();
        private Stopwatch sw_ai2Update = new Stopwatch();
        private Stopwatch sw_ai3Update = new Stopwatch();

        private double d_totalUpdateMs;
        private double d_camUpdateMs;
        private double d_rendererUpdateMs;
        private double d_lightUpdateMs;
        private double d_objUpdateMs;
        private double d_playerUpdateMs;
        private double d_procUpdateMs;
        private double d_levelUpdateMs;
        private double d_particleUpdateMs;
        private double d_formUpdateMs;
        private double d_syncUpdateMs;
        private double d_ai1UpdateMs;
        private double d_ai2UpdateMs;
        private double d_ai3UpdateMs;
        

        public double ShadowRenderMs
        {
            get { return d_shadowRenderMs; }
        }

        public double GBufferRenderMs
        {
            get { return d_gbufferRenderMs; }
        }

        public bool Update
        {
            get { return b_update; }
            set { b_update = value; }
        }

        public double LightRenderMs
        {
            get { return d_lightRenderMs; }
        }

        public double ReconstructShadingRenderMs
        {
            get { return d_reconstructShadingRenderMs; }
        }

        public double SSAORenderMs
        {
            get { return d_ssaoRenderMs; }
        }

        public double PostProcessRenderMs
        {
            get { return d_postProccessRenderMs; }
        }

        public double TotalUpdateMs
        {
            get { return d_totalUpdateMs; }
        }

        public double RendererUpdateMs
        {
            get { return d_rendererUpdateMs; }
        }

        public double CameraUpdateMs
        {
            get { return d_camUpdateMs; }
        }

        public double LightUpdateMs
        {
            get { return d_lightUpdateMs; }
        }

        public double ObjUpdateMs
        {
            get { return d_objUpdateMs; }
        }

        public double PlayerUpdateMs
        {
            get { return d_playerUpdateMs; }
        }

        public double ProcUpdateMs
        {
            get { return d_procUpdateMs; }
        }

        public double ParticleUpdateMs
        {
            get { return d_particleUpdateMs; }
        }

        public double LevelUpdateMs
        {
            get { return d_levelUpdateMs; }
        }

        public double FormUpdateMs
        {
            get { return d_formUpdateMs; }
        }

        public double SyncUpdateMs
        {
            get { return d_syncUpdateMs; }
        }

        public double Ai1UpdateMs
        {
            get { return d_ai1UpdateMs; }
        }

        public double Ai2UpdateMs
        {
            get { return d_ai2UpdateMs; }
        }

        public double Ai3UpdateMs
        {
            get { return d_ai3UpdateMs; }
        }

        public DebugTimings()
        {

        }

        public void StartShadowRenderSw()
        {
            if (b_update)
                sw_shadowRender.Restart();
        }

        public void EndShadowRenderSw()
        {
            if (b_update)
            {
                sw_shadowRender.Stop();
                d_shadowRenderMs = sw_shadowRender.Elapsed.TotalMilliseconds;
            }
            else
                d_shadowRenderMs = 0.0;
        }

        public void StartGBufferRenderSw()
        {
            if (b_update)
                sw_gbufferRender.Restart();
        }

        public void EndGBufferRenderSw()
        {
            if (b_update)
            {
                sw_gbufferRender.Stop();
                d_gbufferRenderMs = sw_gbufferRender.Elapsed.TotalMilliseconds;
            }
            else
                d_gbufferRenderMs = 0.0;
        }

        public void StartLightRenderSw()
        {
            if (b_update)
                sw_lightRender.Restart();
        }

        public void EndLightRenderSw()
        {
            if (b_update)
            {
                sw_lightRender.Stop();
                d_lightRenderMs = sw_lightRender.Elapsed.TotalMilliseconds;
            }
            else
                d_lightRenderMs = 0.0;
        }

        public void StartReconstructShadingSw()
        {
            if (b_update)
                sw_reconstructShadingRender.Restart();
        }

        public void EndReconstructShadingSw()
        {
            if (b_update)
            {
                sw_reconstructShadingRender.Stop();
                d_reconstructShadingRenderMs = sw_reconstructShadingRender.Elapsed.TotalMilliseconds;
            }
            else
                d_reconstructShadingRenderMs = 0.0;
        }

        public void StartSsaoRenderSw()
        {
            if (b_update)
                sw_ssaoRender.Restart();
        }

        public void EndSsaoRenderSw()
        {
            if (b_update)
            {
                sw_ssaoRender.Stop();
                d_ssaoRenderMs = sw_ssaoRender.Elapsed.TotalMilliseconds;
            }
            else
                d_ssaoRenderMs = 0.0;
        }

        public void StartPostProccessRender()
        {
            if (b_update)
                sw_postProcessRender.Restart();
        }

        public void EndPostProccessRender()
        {
            if (b_update)
            {
                sw_postProcessRender.Stop();
                d_postProccessRenderMs = sw_postProcessRender.Elapsed.TotalMilliseconds;
            }
            else
            {
                d_postProccessRenderMs = 0.0;
            }
        }

        public void StartTotalRenderSw()
        {
            if (b_update)
                sw_totalRender.Restart();
        }

        public double EndTotalRenderSw()
        {
            if (b_update)
            {
                sw_totalRender.Stop();
                return sw_totalRender.Elapsed.TotalMilliseconds;
            }
            else
                return 0.0;
        }

        public void StartTotalUpdateSw()
        {
            if (b_update)
                sw_totalUpdate.Restart();
        }

        public void EndTotalUpdateSw()
        {
            if (b_update)
            {
                sw_totalUpdate.Stop();
                d_totalUpdateMs = sw_totalUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_totalUpdateMs = 0.0;
        }

        public void StartCameraUpdateSw()
        {
            if (b_update)
                sw_camUpdate.Restart();
        }

        public void EndCameraUpdateSw()
        {
            if (b_update)
            {
                sw_camUpdate.Stop();
                d_camUpdateMs = sw_camUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_camUpdateMs = 0.0;
        }

        public void StartRendererUpdateSw()
        {
            if (b_update)
                sw_rendererUpdate.Restart();
        }

        public void EndRendererUpdateSw()
        {
            if (b_update)
            {
                sw_rendererUpdate.Stop();
                d_rendererUpdateMs = sw_rendererUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_rendererUpdateMs = 0.0;
        }

        public void StartLightUpdateSw()
        {
            if (b_update)
                sw_lightUpdate.Restart();
        }

        public void EndLightUpdateSw()
        {
            if (b_update)
            {
                sw_lightUpdate.Stop();
                d_lightUpdateMs = sw_lightUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_lightUpdateMs = 0.0;
        }

        public void StartObjUpdateSw()
        {
            if (b_update)
                sw_objUpdate.Restart();
        }

        public void EndObjUpdateSw()
        {
            if (b_update)
            {
                sw_objUpdate.Stop();
                d_objUpdateMs = sw_objUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_objUpdateMs = 0.0;
        }

        public void StartPlayerUpdateSw()
        {
            if (b_update)
                sw_playerUpdate.Restart();
        }

        public void EndPlayerUpdateSw()
        {
            if (b_update)
            {
                sw_playerUpdate.Stop();
                d_playerUpdateMs = sw_playerUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_playerUpdateMs = 0.0;
        }

        public void StartProcUpdateSw()
        {
            if (b_update)
                sw_procUpdate.Restart();
        }

        public void EndProcUpdateSw()
        {
            if (b_update)
            {
                sw_procUpdate.Stop();
                d_procUpdateMs = sw_procUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_procUpdateMs = 0.0;
        }

        public void StartLevelUpdateSw()
        {
            if (b_update)
                sw_levelUpdate.Restart();
        }

        public void EndLevelUpdateSw()
        {
            if (b_update)
            {
                sw_levelUpdate.Stop();
                d_levelUpdateMs = sw_levelUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_levelUpdateMs = 0.0;
        }

        public void StartParticleUpdateSw()
        {
            if (b_update)
                sw_particleUpdate.Restart();
        }

        public void EndParticleUpdateSw()
        {
            if (b_update)
            {
                sw_particleUpdate.Stop();
                d_particleUpdateMs = sw_particleUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_particleUpdateMs = 0.0;
        }

        public void StartFormUpdateSw()
        {
            if (b_update)
                sw_formUpdate.Restart();
        }

        public void EndFormUpdateSw()
        {
            if (b_update)
            {
                sw_formUpdate.Stop();
                d_formUpdateMs = sw_formUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_formUpdateMs = 0.0;
        }

        public void StartSyncUpdateSw()
        {
            if (b_update)
                sw_syncUpdate.Restart();
        }

        public void EndSyncUpdateSw()
        {
            if (b_update)
            {
                sw_syncUpdate.Stop();
                d_syncUpdateMs = sw_syncUpdate.Elapsed.TotalMilliseconds;
            }
            else
                d_syncUpdateMs = 0.0;
        }

        public void StartAi1UpdateSw()
        {
            if (b_update)
                sw_ai1Update.Restart();
        }

        public void EndAi1UpdateSw()
        {
            if (b_update)
            {
                sw_ai1Update.Stop();
                d_ai1UpdateMs = sw_ai1Update.Elapsed.TotalMilliseconds;
            }
            else
                d_ai1UpdateMs = 0.0;
        }

        public void StartAi2UpdateSw()
        {
            if (b_update)
                sw_ai2Update.Restart();
        }

        public void EndAi2UpdateSw()
        {
            if (b_update)
            {
                sw_ai2Update.Stop();
                d_ai2UpdateMs = sw_ai2Update.Elapsed.TotalMilliseconds;
            }
            else
                d_ai2UpdateMs = 0.0;
        }

        public void StartAi3UpdateSw()
        {
            if (b_update)
                sw_ai3Update.Restart();
        }

        public void EndAi3UpdateSw()
        {
            if (b_update)
            {
                sw_ai3Update.Stop();
                d_ai3UpdateMs = sw_ai3Update.Elapsed.TotalMilliseconds;
            }
            else
                d_ai3UpdateMs = 0.0;
        }
    }
}
