using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Words_With_Kinect.Memory_Game
{
    public class MemoryCard
    {
        protected KinectTileButton tile;
        protected Image image;
        protected bool flipped;
        protected int matchNumber;

        public MemoryCard(KinectTileButton tile, Image img, int matchNum)
        {
            this.tile = tile;
            this.image = img;
            this.flipped = false;
            this.matchNumber = matchNum;
            this.image.Visibility = Visibility.Hidden;
        }

        
    }
}
