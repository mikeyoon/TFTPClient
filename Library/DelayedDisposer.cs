﻿/*

Copyright (c) 2010 Jean-Paul Mikkers

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/
using System;
using System.Threading;

namespace CodePlex.JPMikkers.TFTP.Client
{
    public class DelayedDisposer
    {
        private Timer m_Timer;

        private DelayedDisposer(IDisposable obj, int timeOut)
        {
            m_Timer = new Timer(x => 
            {
                try
                {

                    obj.Dispose();
                    m_Timer.Dispose();
                }
                catch
                {
                }
            });
            m_Timer.Change(timeOut, Timeout.Infinite);
        }

        public static void QueueDelayedDispose(IDisposable obj, int timeOut)
        {
            new DelayedDisposer(obj, timeOut);
        }
    }
}