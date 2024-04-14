using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace x360ce.Engine.Maps
{
    public class TapZone
    {
        public TapZone(float tapValue, short minCycles=1)
        {
            this.tapValue = tapValue;
            this.minCycles = minCycles;
        }
        private float tapValue;
        private short cycle = 0;
        private short minCycles;
        private float cyclesPressed = 0f;
        private float cyclesReleased = 0f;
        private bool isPressed = false;

        public float GetOutput(float targetValue)
        {
            //TODO: Handle transition from positive to negative target

            cycle++;
            float average = 0;
            float cyclesTotal = cyclesPressed + cyclesReleased;

            //calculate current average
            if (cyclesTotal > 0) 
            {
                average = cyclesPressed * tapValue / cyclesTotal;
            }

            int targetSign = Math.Sign(targetValue);
            float absTargetValue = Math.Abs(targetValue);

            bool needToPressLonger = isPressed && cyclesPressed < minCycles;
            bool needToReleaseLonger = !isPressed && cyclesReleased < minCycles && cycle != 1;
            if ( needToPressLonger || !needToReleaseLonger &&  absTargetValue > 0 && average <= absTargetValue)
            {
                //Press
                if (!isPressed) {
                    isPressed = true;
                    if (absTargetValue > 0) {
                        float optimalReleaseCycles = (cyclesPressed * (tapValue - absTargetValue)) / absTargetValue;
                        cyclesReleased = cyclesReleased - optimalReleaseCycles;
                        cyclesPressed = 0;
                    }
                }
                
                cyclesPressed += 1;
                return tapValue * targetSign;
            } else {
                //Release
                if(isPressed)
                {
                    isPressed = false;
                    if (tapValue > absTargetValue)
                    { 
                        float optimalPressCycles = (absTargetValue * cyclesReleased/(tapValue - absTargetValue));
                        cyclesPressed = cyclesPressed - optimalPressCycles;
                        cyclesReleased = 0;
                    }
                }

                cyclesReleased += 1;
                return 0;
            }
        }
    }
}
