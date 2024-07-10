using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using static SL_PicamSample.Picam;

namespace SL_PicamSample
{
    class Picam
    {
        [StructLayout(LayoutKind.Sequential, Size = 64)]
        public struct TempSensor
        {
        }
        [StructLayout(LayoutKind.Sequential, Size = 64)]
        public struct TempSerial
        {
        }

        public struct PicamCameraID
        {
            public int model;
            public int computer_interface;
            public TempSensor sensor_name;
            public TempSerial serial_number;

            public string GetSensor()
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = Marshal.AllocHGlobal(64);
                    Marshal.StructureToPtr(sensor_name, ptr, false);
                    return Marshal.PtrToStringAnsi(ptr);
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(ptr);
                    }
                }
            }
        }

        public struct PicamAvailableData
        {
            public IntPtr initial_readout;
            public long readout_count;
        }

        public struct PicamAcquisitionStatus
        {
            public bool running;
            public int errors;
            public double readout_rate;
        }

        public struct PicamAcquisitionBuffer
        {
            public IntPtr memory;
            public long memory_size;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PicamRoi
        {
            public int x;
            public int width;
            public int x_binning;
            public int y;
            public int height;
            public int y_binning;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PicamRois
        {
            public IntPtr roi_array;
            public int roi_count;
        }

        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_InitializeLibrary();
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_UninitializeLibrary();
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_OpenFirstCamera(ref IntPtr camera);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_ConnectDemoCamera(int model, char[] serial_number, ref PicamCameraID id);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_OpenCamera(ref PicamCameraID id, ref IntPtr camera);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_GetCameraID(IntPtr camera, ref PicamCameraID id);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int PicamAdvanced_GetCameraDevice(IntPtr camera, ref IntPtr device);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_StartAcquisition(IntPtr camera);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_WaitForAcquisitionUpdate(IntPtr camera, int readout_time_out, ref PicamAvailableData available, ref PicamAcquisitionStatus status);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_SetParameterLargeIntegerValue(IntPtr camera, int parameter, int value);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_CommitParameters(IntPtr camera, ref int[] failed_parameter_array, ref int failed_parameter_count);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_GetParameterFloatingPointValue(IntPtr camera, int parameter, ref double value);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_GetParameterIntegerValue(IntPtr camera, int parameter, ref int value);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int PicamAdvanced_SetAcquisitionBuffer(IntPtr device, ref PicamAcquisitionBuffer buffer);

        //[DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        //public static extern int Picam_GetParameterRoisConstraint(IntPtr camera, int parameter, int ConstraintCategory,
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_GetParameterRoisValue(IntPtr device, int parameter, out IntPtr region);

        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_SetParameterRoisValue(IntPtr device, int parameter, ref PicamRois region);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void Picam_DestroyRois(ref PicamRois rois);

        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int PicamAdvanced_GetAcquisitionBuffer(IntPtr device, ref PicamAcquisitionBuffer buffer);
        [DllImport("C:\\Program Files\\Common Files\\Princeton Instruments\\Picam\\Runtime\\Picam.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int Picam_CloseCamera(IntPtr camera);

        public static int ParameterValue(int v, int c, int n)
        {
            return ((c << 24) + (v << 16) + n);
        }

        // For single ROI, 3 functions related to Rois should be 'extern'
        public static void DoSingleROI(IntPtr camera)
        {
            int err = 0;
            int failedCount = 0;
            int[] failedArray = new int[2]; //increase size if error thrown for too little size
          

            IntPtr regionPtr;
            //ParameterValue(5, 4, 37) is 'PicamParameter_Rois= PI_V(Rois,Rois,37),' in picam.h
            err = Picam_GetParameterRoisValue(camera, ParameterValue(5, 4, 37), out regionPtr);

            if (err == 0)
            {
                PicamRois region = Marshal.PtrToStructure<PicamRois>(regionPtr);
                // if user didn't set rois of camera, it has only 1 roi count 
                if (region.roi_count == 1)
                {
                    // Marshal the ROI array pointer to the managed array
                    PicamRoi[] roiArray = new PicamRoi[region.roi_count];
                    for (int i = 0; i < region.roi_count; i++)
                    {
                        IntPtr roiPtr = IntPtr.Add(region.roi_array, i * Marshal.SizeOf(typeof(PicamRoi)));
                        roiArray[i] = Marshal.PtrToStructure<PicamRoi>(roiPtr);
                    }
                    //read original roi info(original sensor's size)
                    Console.WriteLine("\nRead ROI value");
                    Console.WriteLine("\nWidth:" + roiArray[0].width.ToString());
                    Console.WriteLine("\nHeight:" + roiArray[0].height.ToString());
                    Console.WriteLine("\nX:" + roiArray[0].x.ToString());
                    Console.WriteLine("\nY:" + roiArray[0].y.ToString());

                    // you should input proper value for roi can support(if not, roi values are same as original one)
                    // The absolute size of the ROI
                    roiArray[0].height = 256;
                    roiArray[0].width = 320;

                    // The offset into the chip of the ROI (1/4th)
                    roiArray[0].x = 320/2;
                    roiArray[0].y = 256/2;

                    // The vertical and horizontal binning
                    roiArray[0].x_binning = 1;
                    roiArray[0].y_binning = 1;

                    // Marshal the modified ROI array back to the unmanaged memory
                    for (int i = 0; i < region.roi_count; i++)
                    {
                        IntPtr roiPtr = IntPtr.Add(region.roi_array, i * Marshal.SizeOf(typeof(PicamRoi)));
                        Marshal.StructureToPtr(roiArray[i], roiPtr, false);
                    }

                    // Set the region of interest
                    err = Picam_SetParameterRoisValue(camera, ParameterValue(5, 4, 37), ref region);
                    if (err == 0)
                    {
                        Picam_CommitParameters(camera, ref failedArray, ref failedCount);

                    }


                    // Clean up the allocated memory
                    Picam_DestroyRois(ref region);


                }

            }


        }


        public static void Acquire(IntPtr device, int frames)
        {
            int error;
            int failedCount = 0;
            int[] failedArray = new int[2]; //increase size if error thrown for too little size
            int readStride = 0;
            int frameSize = 0;
            int offset = 0;
            PicamAvailableData available = new PicamAvailableData();
            PicamAcquisitionStatus status = new PicamAcquisitionStatus();
            PicamAcquisitionBuffer buffer = new PicamAcquisitionBuffer();
            PicamAcquisitionBuffer createdBuffer = new PicamAcquisitionBuffer();
            error = Picam_SetParameterLargeIntegerValue(device, ParameterValue(6, 2, 40), frames);
            //Console.Write("Set Parameter Error: " + error.ToString());
            error = Picam_CommitParameters(device, ref failedArray, ref failedCount);
            //Console.WriteLine(" Commit Error: " + error.ToString() + " Failed Count: " + failedCount.ToString());
            Picam_GetParameterIntegerValue(device, ParameterValue(1, 1, 45), ref readStride);
            byte[] frame = new byte[readStride];
            IntPtr circBuff = Marshal.AllocHGlobal(readStride * 3);
            UInt16[] frameVal = new UInt16[readStride / 2];
            buffer.memory = circBuff;
            buffer.memory_size = readStride * 3;


            error = PicamAdvanced_SetAcquisitionBuffer(device, ref buffer);
            error = PicamAdvanced_GetAcquisitionBuffer(device, ref createdBuffer);
            Console.WriteLine("Circular Buffer manually set to: " + createdBuffer.memory_size.ToString() + " bytes.\n");

            Picam_StartAcquisition(device);
            Console.WriteLine("Acquiring...");
            do
            {
                // can check whether ROI applied or not with frameSize
                Picam_GetParameterIntegerValue(device, ParameterValue(1, 1, 42), ref frameSize);
                Console.WriteLine("FrameSize " + frameSize.ToString());
                error = Picam_WaitForAcquisitionUpdate(device, 1000, ref available, ref status);
                if (available.readout_count > 0)
                {
                    offset = ((int)available.readout_count - 1) * readStride;
                    Marshal.Copy(available.initial_readout, frame, offset, readStride);
                    Buffer.BlockCopy(frame, 0, frameVal, 0, readStride);
                    Console.WriteLine("\tFrame(s) Captured. Readout Count: " + available.readout_count.ToString() +
                        ". Readout Rate: " + status.readout_rate.ToString("N2") + "fps. Center Pixel Value: " + frameVal[(frameVal.Length) / 2].ToString());
                }

            } while (status.running || error == 32);
            Console.WriteLine("...Acquisition Finished!");
            Marshal.FreeHGlobal(circBuff);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int err;
            Picam.PicamCameraID id = new Picam.PicamCameraID();
            IntPtr cam = new IntPtr(0);
            IntPtr dev = new IntPtr(0);
            err = Picam.Picam_InitializeLibrary();
            err = Picam.Picam_OpenFirstCamera(ref cam);
            if (err != 0)
            {
                err = Picam.Picam_ConnectDemoCamera(2201, "01234567".ToCharArray(), ref id);
                err = Picam.Picam_OpenCamera(ref id, ref cam);
                Console.WriteLine("No Live Camera Found, ***Opened Demo Camera.***");
            }
            else
            {
                err = Picam.Picam_GetCameraID(cam, ref id);
                Console.WriteLine("Live Camera Opened.");
            }
            
        

            Console.WriteLine("Sensor Opened : "+ id.GetSensor());

            //Console.WriteLine("Open Error: " + err.ToString());
            err = Picam.PicamAdvanced_GetCameraDevice(cam, ref dev);
            Picam.DoSingleROI(cam);
            
            Picam.Acquire(dev, 20);

            // Verify whether ROI applied or not
            IntPtr regionPtr;
            err = Picam.Picam_GetParameterRoisValue(cam, ParameterValue(5, 4, 37), out regionPtr);
            if (err == 0)
            {
                PicamRois regionR = Marshal.PtrToStructure<PicamRois>(regionPtr);

                if (regionR.roi_count == 1)
                {
                    // Marshal the ROI array pointer to the managed array
                    PicamRoi[] roiArray = new PicamRoi[regionR.roi_count];
                    for (int i = 0; i < regionR.roi_count; i++)
                    {
                        IntPtr roiPtr = IntPtr.Add(regionR.roi_array, i * Marshal.SizeOf(typeof(PicamRoi)));
                        roiArray[i] = Marshal.PtrToStructure<PicamRoi>(roiPtr);
                    }
                    Console.WriteLine("\nRead ROI value");
                    Console.WriteLine("\nWidth:" + roiArray[0].width.ToString());
                    Console.WriteLine("\nHeight:" + roiArray[0].height.ToString());
                    Console.WriteLine("\nX:" + roiArray[0].x.ToString());
                    Console.WriteLine("\nY:" + roiArray[0].y.ToString());
                }

            }


            err = Picam.Picam_CloseCamera(dev);
            err = Picam.Picam_UninitializeLibrary();
        }
    }
}
