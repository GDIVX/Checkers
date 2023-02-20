using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    internal class ISpiralEnumerator<T> : IEnumerator<T>
    {
        int loopAmount; // amount of times to spiral around the tilemap
        int currentLoop = 0; //what current loop its on
        int stepsPerDirection = 1;
        int currentStep = 0;
        int direction = 0; //0 left, 1 up, 2 right, 3 down
        bool firstRun = true;
        
        int x, y; //tile position in map
        int width;
        int height;
        T[,] map;

        public ISpiralEnumerator(T[,] map)
        {
            this.map = map;
            
            //size of tilemap
            width = map.GetLength(0);
            height = map.GetLength(1);
            
            //start at center-ish of map
            x = width/2;
            y = height/2;
            
            //if height is longer than or equal to the width it should do 1 more loop.
            loopAmount = height / 2 >= width / 2 ? height / 2: width / 2;

        }

        public T Current => map[x, y];

        public bool MoveNext()
        {
            //depending on direction, add or subtract to x or y
            //if current 
            
            if(firstRun)
            {
                firstRun = false;
                return true;
            }

            do
            {
                if(loopAmount < currentLoop)
                {
                    return false;
                }

                if (currentStep == stepsPerDirection)
                {
                    direction++;
                    if (direction % 2 == 0)
                    {
                        stepsPerDirection++;
                        if (direction % 4 == 0)
                        {
                            direction = 0;
                            currentLoop++;
                        }
                    }
                    currentStep = 0;
                }
                
                currentStep++;
                switch (direction)
                {
                    case 0:
                        y--;
                        break;
                    case 1:
                        x--;
                        break;
                    case 2:
                        y++;
                        break;
                    case 3:
                        x++;
                        break;
                }
            }
            while (x >= width || x <0 || y >= height || y < 0);

            return true;

        }


        object IEnumerator.Current => Current;

        public void Dispose() { }

        public void Reset() { }
    }
}
