using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SoftwareRenderer
{
    public class RenderWindow : Form
    {
        private const int RenderWidth = 800;
        private const int RenderHeight = 600;
        private byte[] framebuffer;
        private Bitmap screenBitmap;
        private PictureBox display;

        public RenderWindow()
        {
            this.ClientSize = new Size(RenderWidth, RenderHeight);
            this.Text = "Custom 3D Software Renderer";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            framebuffer = new byte[RenderWidth * RenderHeight * 4];
            screenBitmap = new Bitmap(RenderWidth, RenderHeight, PixelFormat.Format32bppPArgb);

            display = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = screenBitmap,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(display);

            System.Windows.Forms.Timer gameLoop = new System.Windows.Forms.Timer { Interval = 16 };
            gameLoop.Tick += (s, e) => RenderFrame();
            gameLoop.Start();
        }

        // --- 1. THE GAME LOOP (CUBE RENDERING) ---
        private void RenderFrame()
        {
            ClearScreen(0, 0, 0);

            // Define the 8 corners of a cube (Width: 1, Height: 1, Depth: 1)
            // We place it at Z: 3.0 so it is sitting in front of our camera
            Vector3[] vertices = new Vector3[]
            {
                new Vector3(-1, -1,  2), // 0: Bottom-Left-Front
                new Vector3( 1, -1,  2), // 1: Bottom-Right-Front
                new Vector3( 1,  1,  2), // 2: Top-Right-Front
                new Vector3(-1,  1,  2), // 3: Top-Left-Front
                new Vector3(-1, -1,  4), // 4: Bottom-Left-Back
                new Vector3( 1, -1,  4), // 5: Bottom-Right-Back
                new Vector3( 1,  1,  4), // 6: Top-Right-Back
                new Vector3(-1,  1,  4)  // 7: Top-Left-Back
            };

            // Draw the Front Face (Green)
            DrawLine3D(vertices[0], vertices[1], 0, 255, 0);
            DrawLine3D(vertices[1], vertices[2], 0, 255, 0);
            DrawLine3D(vertices[2], vertices[3], 0, 255, 0);
            DrawLine3D(vertices[3], vertices[0], 0, 255, 0);

            // Draw the Back Face (Red)
            DrawLine3D(vertices[4], vertices[5], 255, 0, 0);
            DrawLine3D(vertices[5], vertices[6], 255, 0, 0);
            DrawLine3D(vertices[6], vertices[7], 255, 0, 0);
            DrawLine3D(vertices[7], vertices[4], 255, 0, 0);

            // Connect the Front and Back faces (Blue)
            DrawLine3D(vertices[0], vertices[4], 0, 100, 255);
            DrawLine3D(vertices[1], vertices[5], 0, 100, 255);
            DrawLine3D(vertices[2], vertices[6], 0, 100, 255);
            DrawLine3D(vertices[3], vertices[7], 0, 100, 255);

            PushFramebufferToScreen();
        }

        // --- 2. 3D MATH & PROJECTION ---
        private void DrawLine3D(Vector3 p1, Vector3 p2, byte r, byte g, byte b)
        {
            float fov = 400.0f;

            if (p1.Z <= 0.1f || p2.Z <= 0.1f) return;

            int x0 = (int)((p1.X / p1.Z) * fov) + (RenderWidth / 2);
            int y0 = (int)((p1.Y / p1.Z) * fov) + (RenderHeight / 2);

            int x1 = (int)((p2.X / p2.Z) * fov) + (RenderWidth / 2);
            int y1 = (int)((p2.Y / p2.Z) * fov) + (RenderHeight / 2);

            DrawLine(x0, y0, x1, y1, r, g, b);
        }

        // --- 3. BRESENHAM'S LINE ALGORITHM ---
        private void DrawLine(int x0, int y0, int x1, int y1, byte r, byte g, byte b)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                DrawPixel(x0, y0, r, g, b);

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        // --- 4. CORE GRAPHICS ---
        private void DrawPixel(int x, int y, byte r, byte g, byte b)
        {
            if (x < 0 || x >= RenderWidth || y < 0 || y >= RenderHeight) return;
            int index = (y * RenderWidth + x) * 4;
            framebuffer[index] = b;         
            framebuffer[index + 1] = g;     
            framebuffer[index + 2] = r;     
            framebuffer[index + 3] = 255;   
        }

        private void ClearScreen(byte r, byte g, byte b)
        {
            for (int i = 0; i < framebuffer.Length; i += 4)
            {
                framebuffer[i] = b;
                framebuffer[i + 1] = g;
                framebuffer[i + 2] = r;
                framebuffer[i + 3] = 255;
            }
        }

        private void PushFramebufferToScreen()
        {
            BitmapData bmpData = screenBitmap.LockBits(
                new Rectangle(0, 0, screenBitmap.Width, screenBitmap.Height),
                ImageLockMode.WriteOnly,
                screenBitmap.PixelFormat);
            Marshal.Copy(framebuffer, 0, bmpData.Scan0, framebuffer.Length);
            screenBitmap.UnlockBits(bmpData);
            display.Invalidate(); 
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RenderWindow());
        }
    }

    // --- 5. DATA STRUCTURES ---
    public struct Vector3
    {
        public float X, Y, Z;
        public Vector3(float x, float y, float z) 
        { 
            X = x; Y = y; Z = z; 
        }
    }
}