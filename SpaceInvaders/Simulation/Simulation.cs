using System.Diagnostics;

namespace SpaceInvaders
{
    class Simulation
    {
        public enum State
        {
            RealTime,
            FixedStep,
            SingleStep,
            Pause
        }

        private static Simulation instance = null;
        private State state;

        float stopWatch_tic;
        float stopWatch_toc;
        float totalWatch;
        float timeStep;
        const float SIM_SINGLE_TIME_STEP = 0.016666f;

        private Simulation()
        {
            this.state = State.SingleStep;

            this.timeStep = 0.0f;
            this.totalWatch = 0.0f;
            this.stopWatch_tic = 0.0f;
            this.stopWatch_toc = 0.0f;
        }

        public static void Create()
        {
            Debug.Assert(instance == null);
            if (instance == null)
            {
                instance = new Simulation();
            }
        }

        public static void SetState(State simState)
        {
            Simulation s = GetSimulation();
            Debug.Assert(s != null);
            s.SetSimState(simState);
        }

        public static State GetState()
        {
            Simulation s = GetSimulation();
            Debug.Assert(s != null);
            return s.GetSimState();
        }

        public static float GetTimeStep()
        {
            Simulation s = GetSimulation();
            Debug.Assert(s != null);
            return s.timeStep;
        }

        public static float GetTotalTime()
        {
            Simulation s = GetSimulation();
            Debug.Assert(s != null);
            return s.totalWatch;
        }

        public static void Update(float sysTime)
        {
            Simulation sim = GetSimulation();
            Debug.Assert(sim != null);
            sim.stopWatch_toc = sysTime - sim.stopWatch_tic;
            sim.stopWatch_tic = sysTime;
            
            if (sim.GetSimState() == State.FixedStep)
            {
                sim.timeStep = SIM_SINGLE_TIME_STEP;
            }
            else if (sim.GetSimState() == State.RealTime)
            {
                sim.timeStep = sim.stopWatch_toc;
            }
            else if (sim.GetSimState() == State.SingleStep)
            {
                sim.timeStep = SIM_SINGLE_TIME_STEP;
                sim.SetSimState(State.Pause);
            }
            else //  pSim->getState() == SimulationEnum::Pause
            {
                sim.timeStep = 0.0f;
            }
            sim.totalWatch += sim.timeStep;
        }

        private void SetSimState(State simState)
        {
            state = simState;
        }

        private State GetSimState()
        {
            return this.state;
        }
        private static Simulation GetSimulation()
        {
            Debug.Assert(instance != null);
            return instance;
        }

        

    }
}
