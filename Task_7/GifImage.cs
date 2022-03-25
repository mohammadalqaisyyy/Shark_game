﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

//Mohammad Al-Qaisy

namespace Task_7
{
    

public class GifImage
{
    private Image gifImage;
    private FrameDimension dimension;
    private int frameCount;
    private int currentFrame = -1;
    private bool reverse;
    private int step = 1;

    public GifImage(string path)
    {
        gifImage = Image.FromFile(path);
        //initialize
        dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
        //gets the GUID
        //total frames in the animation
        frameCount = gifImage.GetFrameCount(dimension);
    }

    public bool ReverseAtEnd {
        //whether the gif should play backwards when it reaches the end
        get { return reverse; }
        set { reverse = value; }
    }

    public Image GetNextFrame()
    {

        currentFrame += step+8;

        //if the animation reaches a boundary...
        if (currentFrame >= frameCount || currentFrame < 0) {
            if (reverse) {
                step *= -1;
                //...reverse the count
                //apply it
                currentFrame += step;
            }
            else {
                currentFrame = 0;
                //...or start over
            }
        }
        return GetFrame(currentFrame);
    }

    public Image GetFrame(int index)
    {
        gifImage.SelectActiveFrame(dimension, index);
        //find the frame
        return (Image)gifImage.Clone();
        //return a copy of it
    }
}
}
