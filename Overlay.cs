namespace External_ESP_Base
{
    using ECHELON;
    using External_ESP_base;
    using ProtoBuf;
    using Rust_Interceptor;
    using Rust_Interceptor.Data;
    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;
    using SharpDX.DXGI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using UnityEngine;

    public class Overlay : Form
    {
        private static uint[] angryAnimalIds = new uint[5];
        private static SortedDictionary<uint, ProtoBuf.Entity> angryAnimalTable = new SortedDictionary<uint, ProtoBuf.Entity>();
        private IContainer components;
        private WindowRenderTarget device;
        private static ProtoBuf.Entity entity;
        private static Rust_Interceptor.Data.Entity.EntityUpdate entityData = null;
        private static SortedDictionary<uint, ProtoBuf.Entity> entityTable = new SortedDictionary<uint, ProtoBuf.Entity>();
        private static List<Rust_Interceptor.Data.Entity.EntityUpdate> entityUpdate = new List<Rust_Interceptor.Data.Entity.EntityUpdate>();
        private bool ESP_Bone = true;
        private bool ESP_Box = true;
        private bool ESP_Distance;
        private bool ESP_Health;
        private bool ESP_Name;
        private SharpDX.Direct2D1.Factory factory;
        private TextFormat font;
        private SharpDX.DirectWrite.Factory fontFactory;
        private const string fontFamily = "Tahoma";
        private const float fontSize = 18f;
        private const float fontSizeRadarSmall = 10f;
        private const float fontSizeSmall = 14f;
        private TextFormat fontSmall;
        private static int frameRate;
        private IntPtr handle;
        private bool IsMinimized;
        private bool IsResize;
        private static int lastFrameRate;
        private static int lastTick;
        private static ProtoBuf.Entity localPlayer;
        private KeysManager manager;
        private static uint[] niceAnimalIds = new uint[4];
        private static SortedDictionary<uint, ProtoBuf.Entity> niceAnimalTable = new SortedDictionary<uint, ProtoBuf.Entity>();
        private List<Player> players;
        private static SortedDictionary<uint, ProtoBuf.Entity> playerTable = new SortedDictionary<uint, ProtoBuf.Entity>();
        private Process process;
        private static bool proxyisAlive = false;
        private TextFormat radarSmall;
        private SharpDX.Rectangle rect;
        private HwndRenderTargetProperties renderProperties;
        private static uint[] rockIds = new uint[6];
        private static SortedDictionary<uint, ProtoBuf.Entity> rockTable = new SortedDictionary<uint, ProtoBuf.Entity>();
        private SolidColorBrush solidColorBrush;
        private static SortedDictionary<uint, ProtoBuf.Entity> tcTable = new SortedDictionary<uint, ProtoBuf.Entity>();
        private Thread updateStream;
        private Thread windowStream;

        public Overlay(Process process)
        {
            this.process = process;
            this.handle = base.Handle;
            int windowLong = Managed.GetWindowLong(base.Handle, -20);
            Managed.SetWindowLong(base.Handle, -20, (windowLong | 0x80000) | 0x20);
            IntPtr hWndInsertAfter = new IntPtr(-1);
            Managed.SetWindowPos(base.Handle, hWndInsertAfter, 0, 0, 0, 0, 3);
            this.OnResize(null);
            this.InitializeComponent();
        }

        public int CalculateFrameRate()
        {
            int tickCount = System.Environment.TickCount;
            if ((tickCount - lastTick) >= 0x3e8)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = tickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawCircle(int X, int Y, int W, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawEllipse(new Ellipse(new SharpDX.Vector2((float) X, (float) Y), (float) W, (float) W), this.solidColorBrush);
        }

        private void DrawEntity(WindowRenderTarget device, ProtoBuf.Entity localPlay, ProtoBuf.Entity entity, SharpDX.Vector2 screenMid, SharpDX.Color color)
        {
            if (entity != null)
            {
                SharpDX.Vector2 pointToRotate = SharpDX.Vector2.op_UnaryPlus(new SharpDX.Vector2(entity.baseEntity.pos.x, entity.baseEntity.pos.z));
                pointToRotate.X = localPlay.baseEntity.pos.x - entity.baseEntity.pos.x;
                pointToRotate.Y = localPlay.baseEntity.pos.z - entity.baseEntity.pos.z;
                if (pointToRotate.Length() <= 149f)
                {
                    if (pointToRotate.Length() > 150f)
                    {
                        pointToRotate.Normalize();
                        pointToRotate = (SharpDX.Vector2) (pointToRotate * 150f);
                    }
                    pointToRotate += screenMid;
                    pointToRotate = RotatePoint(pointToRotate, screenMid, localPlayer.baseEntity.rot.y);
                    this.FillEllipse(device, new SharpDX.Color(0xff, 0xff, 0xff, color.A), pointToRotate.X + 1.5f, pointToRotate.Y + 1.5f, 6f, 6f, true);
                    this.FillEllipse(device, color, pointToRotate.X + 1f, pointToRotate.Y + 1f, 4f, 4f, true);
                }
            }
        }

        private void DrawFillCircle(int X, int Y, int W, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.FillEllipse(new Ellipse(new SharpDX.Vector2((float) X, (float) Y), (float) W, (float) W), this.solidColorBrush);
        }

        private void DrawFillRect(int X, int Y, int W, int H, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.FillRectangle(new SharpDX.RectangleF((float) X, (float) Y, (float) W, (float) H), this.solidColorBrush);
        }

        private void DrawHealth(int X, int Y, int W, int H, int Health, int MaxHealth)
        {
            if (Health <= 0)
            {
                Health = 1;
            }
            if (MaxHealth < Health)
            {
                MaxHealth = 100;
            }
            int num = (int) (((float) Health) / (((float) MaxHealth) / 100f));
            int num2 = (int) ((((float) W) / 100f) * num);
            if (num2 <= 2)
            {
                num2 = 3;
            }
            SharpDX.Color color = new SharpDX.Color(0xff, 0, 0, 0xff);
            if (num >= 20)
            {
                color = new SharpDX.Color(0xff, 0xa5, 0, 0xff);
            }
            if (num >= 40)
            {
                color = new SharpDX.Color(0xff, 0xff, 0, 0xff);
            }
            if (num >= 60)
            {
                color = new SharpDX.Color(0xad, 0xff, 0x2f, 0xff);
            }
            if (num >= 80)
            {
                color = new SharpDX.Color(0, 0xff, 0, 0xff);
            }
            this.DrawFillRect(X, Y - 1, W + 1, H + 2, SharpDX.Color.Black);
            this.DrawFillRect(X + 1, Y, num2 - 1, H, color);
        }

        private void DrawImage(int X, int Y, int W, int H, SharpDX.Direct2D1.Bitmap bitmap)
        {
            this.device.DrawBitmap(bitmap, new SharpDX.RectangleF((float) X, (float) Y, (float) W, (float) H), 1f, BitmapInterpolationMode.Linear);
        }

        private void DrawImage(int X, int Y, int W, int H, SharpDX.Direct2D1.Bitmap bitmap, float angle)
        {
            this.device.Transform = Matrix3x2.Rotation(angle, new SharpDX.Vector2((float) (X + (H / 2)), (float) (Y + (H / 2))));
            this.device.DrawBitmap(bitmap, new SharpDX.RectangleF((float) X, (float) Y, (float) W, (float) H), 1f, BitmapInterpolationMode.Linear);
            this.device.Transform = Matrix3x2.Rotation(0f);
        }

        private void DrawLine(SharpDX.Vector3 w2s, SharpDX.Vector3 _w2s, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawLine(new SharpDX.Vector2(w2s.X, w2s.Y), new SharpDX.Vector2(_w2s.X, _w2s.Y), this.solidColorBrush);
        }

        private void DrawLine(int X, int Y, int XX, int YY, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawLine(new SharpDX.Vector2((float) X, (float) Y), new SharpDX.Vector2((float) XX, (float) YY), this.solidColorBrush);
        }

        private void DrawPlayer(WindowRenderTarget device, ProtoBuf.Entity currentPlayer, ProtoBuf.Entity player, SharpDX.Vector2 screenMid, SharpDX.Color color)
        {
            if ((player != null) && (player.basePlayer.name != currentPlayer.basePlayer.name))
            {
                SharpDX.Vector2 pointToRotate = SharpDX.Vector2.op_UnaryPlus(new SharpDX.Vector2(player.baseEntity.pos.x, player.baseEntity.pos.z));
                pointToRotate.X = currentPlayer.baseEntity.pos.x - player.baseEntity.pos.x;
                pointToRotate.Y = currentPlayer.baseEntity.pos.z - player.baseEntity.pos.z;
                if (pointToRotate.Length() <= 400f)
                {
                    if (pointToRotate.Length() > 150f)
                    {
                        pointToRotate.Normalize();
                        pointToRotate = (SharpDX.Vector2) (pointToRotate * 150f);
                    }
                    pointToRotate += screenMid;
                    pointToRotate = RotatePoint(pointToRotate, screenMid, currentPlayer.baseEntity.rot.y);
                    if (Radar.Default.drawPlayerNames)
                    {
                        this.DrawText(((int) pointToRotate.X) - player.basePlayer.name.Length, ((int) pointToRotate.Y) + 5, player.basePlayer.name, SharpDX.Color.White, true, this.radarSmall);
                    }
                    this.FillEllipse(device, color, pointToRotate.X + 2f, pointToRotate.Y + 2f, 8f, 8f, true);
                }
            }
        }

        private void DrawProgress(int X, int Y, int W, int H, int Value, int MaxValue)
        {
            int num = (int) (((float) Value) / (((float) MaxValue) / 100f));
            int num2 = (int) ((((float) W) / 100f) * num);
            SharpDX.Color color = new SharpDX.Color(0, 0xff, 0, 0xff);
            if (num >= 20)
            {
                color = new SharpDX.Color(0xad, 0xff, 0x2f, 0xff);
            }
            if (num >= 40)
            {
                color = new SharpDX.Color(0xff, 0xff, 0, 0xff);
            }
            if (num >= 60)
            {
                color = new SharpDX.Color(0xff, 0xa5, 0, 0xff);
            }
            if (num >= 80)
            {
                color = new SharpDX.Color(0xff, 0, 0, 0xff);
            }
            this.DrawFillRect(X, Y - 1, W + 1, H + 2, SharpDX.Color.Black);
            if (num2 >= 2)
            {
                this.DrawFillRect(X + 1, Y, num2 - 1, H, color);
            }
        }

        private void DrawRadarText(int X, int Y, string text, SharpDX.Color color, bool outline, TextFormat format)
        {
            if (outline)
            {
                this.solidColorBrush.Color = (Color4) SharpDX.Color.Black;
                this.device.DrawText(text, format, new SharpDX.RectangleF((float) (X + 1), (float) (Y + 1), format.FontSize * text.Length, format.FontSize), this.solidColorBrush);
            }
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawText(text, format, new SharpDX.RectangleF((float) X, (float) Y, format.FontSize * text.Length, format.FontSize), this.solidColorBrush);
        }

        private void DrawRect(int X, int Y, int W, int H, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawRectangle((SharpDX.RectangleF) new SharpDX.Rectangle(X, Y, W, H), this.solidColorBrush);
        }

        private void DrawRect(int X, int Y, int W, int H, SharpDX.Color color, float stroke)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawRectangle((SharpDX.RectangleF) new SharpDX.Rectangle(X, Y, W, H), this.solidColorBrush, stroke);
        }

        private void DrawSprite(SharpDX.RectangleF destinationRectangle, SharpDX.Direct2D1.Bitmap bitmap, SharpDX.RectangleF sourceRectangle)
        {
            this.device.DrawBitmap(bitmap, new SharpDX.RectangleF?(destinationRectangle), 1f, BitmapInterpolationMode.Linear, new SharpDX.RectangleF?(sourceRectangle));
        }

        private void DrawSprite(SharpDX.RectangleF destinationRectangle, SharpDX.Direct2D1.Bitmap bitmap, SharpDX.RectangleF sourceRectangle, float angle)
        {
            SharpDX.Vector2 center = new SharpDX.Vector2 {
                X = destinationRectangle.X + (destinationRectangle.Width / 2f),
                Y = destinationRectangle.Y + (destinationRectangle.Height / 2f)
            };
            this.device.Transform = Matrix3x2.Rotation(angle, center);
            this.device.DrawBitmap(bitmap, new SharpDX.RectangleF?(destinationRectangle), 1f, BitmapInterpolationMode.Linear, new SharpDX.RectangleF?(sourceRectangle));
            this.device.Transform = Matrix3x2.Rotation(0f);
        }

        private void DrawText(int X, int Y, string text, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawText(text, this.font, new SharpDX.RectangleF((float) X, (float) Y, this.font.FontSize * text.Length, this.font.FontSize), this.solidColorBrush);
        }

        private void DrawText(int X, int Y, string text, SharpDX.Color color, bool outline)
        {
            if (outline)
            {
                this.solidColorBrush.Color = (Color4) SharpDX.Color.Black;
                this.device.DrawText(text, this.font, new SharpDX.RectangleF((float) (X + 1), (float) (Y + 1), this.font.FontSize * text.Length, this.font.FontSize), this.solidColorBrush);
            }
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawText(text, this.font, new SharpDX.RectangleF((float) X, (float) Y, this.font.FontSize * text.Length, this.font.FontSize), this.solidColorBrush);
        }

        private void DrawText(int X, int Y, string text, SharpDX.Color color, bool outline, TextFormat format)
        {
            if (outline)
            {
                this.solidColorBrush.Color = (Color4) SharpDX.Color.Black;
                this.device.DrawText(text, format, new SharpDX.RectangleF((float) (X + 1), (float) (Y + 1), format.FontSize * text.Length, format.FontSize), this.solidColorBrush);
            }
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawText(text, format, new SharpDX.RectangleF((float) X, (float) Y, format.FontSize * text.Length, format.FontSize), this.solidColorBrush);
        }

        private void DrawTextCenter(int X, int Y, int W, int H, string text, SharpDX.Color color)
        {
            this.solidColorBrush.Color = (Color4) color;
            TextLayout textLayout = new TextLayout(this.fontFactory, text, this.fontSmall, (float) W, (float) H) {
                TextAlignment = SharpDX.DirectWrite.TextAlignment.Center
            };
            this.device.DrawTextLayout(new SharpDX.Vector2((float) X, (float) Y), textLayout, this.solidColorBrush);
            textLayout.Dispose();
        }

        private void DrawTextCenter(int X, int Y, int W, int H, string text, SharpDX.Color color, bool outline)
        {
            TextLayout textLayout = new TextLayout(this.fontFactory, text, this.fontSmall, (float) W, (float) H) {
                TextAlignment = SharpDX.DirectWrite.TextAlignment.Center
            };
            if (outline)
            {
                this.solidColorBrush.Color = (Color4) SharpDX.Color.Black;
                this.device.DrawTextLayout(new SharpDX.Vector2((float) (X + 1), (float) (Y + 1)), textLayout, this.solidColorBrush);
            }
            this.solidColorBrush.Color = (Color4) color;
            this.device.DrawTextLayout(new SharpDX.Vector2((float) X, (float) Y), textLayout, this.solidColorBrush);
            textLayout.Dispose();
        }

        private void DrawWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.updateStream.Abort();
            this.windowStream.Abort();
            System.Environment.Exit(0);
        }

        private void DrawWindow_Load(object sender, EventArgs e)
        {
            base.TopMost = true;
            base.Visible = true;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Width = this.rect.Width;
            base.Height = this.rect.Height;
            base.Name = Process.GetCurrentProcess().ProcessName + "~Overlay";
            this.Text = Process.GetCurrentProcess().ProcessName + "~Overlay";
            this.factory = new SharpDX.Direct2D1.Factory();
            this.fontFactory = new SharpDX.DirectWrite.Factory();
            HwndRenderTargetProperties properties = new HwndRenderTargetProperties {
                Hwnd = base.Handle,
                PixelSize = new Size2(this.rect.Width, this.rect.Height),
                PresentOptions = PresentOptions.None
            };
            this.renderProperties = properties;
            this.device = new WindowRenderTarget(this.factory, new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)), this.renderProperties);
            this.solidColorBrush = new SolidColorBrush(this.device, (Color4) SharpDX.Color.White);
            this.font = new TextFormat(this.fontFactory, "Tahoma", 18f);
            this.fontSmall = new TextFormat(this.fontFactory, "Tahoma", 14f);
            this.radarSmall = new TextFormat(this.fontFactory, "Tahoma", 10f);
            this.updateStream = new Thread(new ParameterizedThreadStart(this.Update));
            this.updateStream.Start();
            this.windowStream = new Thread(new ParameterizedThreadStart(this.SetWindow));
            this.windowStream.Start();
            rockIds[0] = 0x2be3670f;
            rockIds[1] = 0x36a0b2f1;
            rockIds[2] = 0x5092ae0b;
            rockIds[3] = 0x9fcaba99;
            rockIds[4] = 0xaa88067b;
            rockIds[5] = 0x5995cbc1;
            angryAnimalIds[0] = 0xb284f12a;
            angryAnimalIds[1] = 0xf488b0cc;
            niceAnimalIds[0] = 0x3d635c60;
            niceAnimalIds[1] = 0x6f558337;
            niceAnimalIds[2] = 0x15a97e09;
            niceAnimalIds[3] = 0x99c0c431;
        }

        protected void FillEllipse(WindowRenderTarget device, SharpDX.Color color, float x, float y, float width, float height, bool centered = false)
        {
            using (SolidColorBrush brush = new SolidColorBrush(device, (Color4) color))
            {
                device.FillEllipse(new Ellipse(new SharpDX.Vector2(centered ? x : (x + (width / 2f)), centered ? y : (y + (height / 2f))), width / 2f, height / 2f), brush);
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Overlay));
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            base.ClientSize = new Size(10, 10);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "Overlay";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Overlay";
            base.TopMost = true;
            base.TransparencyKey = System.Drawing.Color.Black;
            base.FormClosing += new FormClosingEventHandler(this.DrawWindow_FormClosing);
            base.Load += new EventHandler(this.DrawWindow_Load);
            base.ResumeLayout(false);
        }

        private bool IsGameRun()
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == this.process.ProcessName)
                {
                    return true;
                }
            }
            return false;
        }

        public SharpDX.Vector3 Multiply(SharpDX.Vector3 vector, Matrix mat) => 
            new SharpDX.Vector3(((mat.M11 * vector.X) + (mat.M21 * vector.Y)) + (mat.M31 * vector.Z), ((mat.M12 * vector.X) + (mat.M22 * vector.Y)) + (mat.M32 * vector.Z), ((mat.M13 * vector.X) + (mat.M23 * vector.Y)) + (mat.M33 * vector.Z));

        protected override void OnResize(EventArgs e)
        {
            int[] numArray1 = new int[4];
            numArray1[2] = this.rect.Width;
            numArray1[3] = this.rect.Height;
            int[] pMargins = numArray1;
            Managed.DwmExtendFrameIntoClientArea(base.Handle, ref pMargins);
        }

        private void Read()
        {
        }

        public static void ReadRustPackets(string ip, int port)
        {
            new External_ESP_base.Menu();
            RustInterceptor interceptor = new RustInterceptor(ip, port, 0x162e) {
                ClientPackets = true,
                RememberPackets = true,
                RememberFilteredOnly = false
            };
            interceptor.Start();
            while (interceptor.IsAlive)
            {
                Packet packet;
                proxyisAlive = true;
                interceptor.GetPacket(out packet);
                switch (((Packet.Rust) packet.packetID))
                {
                    case Packet.Rust.EntityPosition:
                    {
                        entityUpdate = Rust_Interceptor.Data.Entity.ParsePositions(packet);
                        ProtoBuf.Entity entity = new ProtoBuf.Entity();
                        ProtoBuf.Entity entity2 = new ProtoBuf.Entity();
                        if (playerTable.ContainsKey(entityUpdate[0].UID))
                        {
                            entity = playerTable[entityUpdate[0].UID];
                            entity.baseEntity.pos = entityUpdate[0].Position;
                            entity.baseEntity.rot = entityUpdate[0].Rotation;
                            playerTable[entityUpdate[0].UID] = entity;
                        }
                        else if (angryAnimalTable.ContainsKey(entityUpdate[0].UID))
                        {
                            entity2 = angryAnimalTable[entityUpdate[0].UID];
                            entity2.baseEntity.pos = entityUpdate[0].Position;
                            entity2.baseEntity.rot = entityUpdate[0].Rotation;
                            angryAnimalTable[entityUpdate[0].UID] = entity2;
                        }
                        else if (niceAnimalTable.ContainsKey(entityUpdate[0].UID))
                        {
                            entity2 = niceAnimalTable[entityUpdate[0].UID];
                            entity2.baseEntity.pos = entityUpdate[0].Position;
                            entity2.baseEntity.rot = entityUpdate[0].Rotation;
                            niceAnimalTable[entityUpdate[0].UID] = entity2;
                        }
                        else if (entityTable.ContainsKey(entityUpdate[0].UID))
                        {
                            entity2 = entityTable[entityUpdate[0].UID];
                            entity2.baseEntity.pos = entityUpdate[0].Position;
                            entity2.baseEntity.rot = entityUpdate[0].Rotation;
                            entityTable[entityUpdate[0].UID] = entity2;
                        }
                        continue;
                    }
                    case Packet.Rust.Entities:
                    {
                        Rust_Interceptor.Data.Entity.ParseEntity(packet, out Overlay.entity);
                        if (Overlay.entity.basePlayer != null)
                        {
                            if (Overlay.entity.basePlayer.metabolism != null)
                            {
                                playerTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                                localPlayer = Overlay.entity;
                            }
                            else
                            {
                                playerTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                            }
                        }
                        else if (rockIds.Contains<uint>(Overlay.entity.baseNetworkable.prefabID))
                        {
                            rockTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                        }
                        else if (Overlay.entity.baseNetworkable.prefabID == 0x4f1f0e9b)
                        {
                            tcTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                        }
                        else if (angryAnimalIds.Contains<uint>(Overlay.entity.baseNetworkable.prefabID))
                        {
                            angryAnimalTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                        }
                        else if (niceAnimalIds.Contains<uint>(Overlay.entity.baseNetworkable.prefabID))
                        {
                            niceAnimalTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                        }
                        else
                        {
                            entityTable[Overlay.entity.baseNetworkable.uid] = Overlay.entity;
                        }
                        continue;
                    }
                    case Packet.Rust.EntityDestroy:
                    {
                        EntityDestroy destroy = new EntityDestroy(packet);
                        if (playerTable.ContainsKey(destroy.UID))
                        {
                            playerTable.Remove(destroy.UID);
                        }
                        else if (rockTable.ContainsKey(destroy.UID))
                        {
                            rockTable.Remove(destroy.UID);
                        }
                        continue;
                    }
                }
            }
        }

        public static SharpDX.Vector2 RotatePoint(SharpDX.Vector2 pointToRotate, SharpDX.Vector2 centerPoint, float angleInDegrees)
        {
            float single1 = (float) ((3.1415926535897931 * angleInDegrees) / 180.0);
            float num = (float) Math.Cos((double) single1);
            float num2 = (float) Math.Sin((double) single1);
            return new SharpDX.Vector2 { 
                X = (int) (((num * (pointToRotate.X - centerPoint.X)) - (num2 * (pointToRotate.Y - centerPoint.Y))) + centerPoint.X),
                Y = (int) (((num2 * (pointToRotate.X - centerPoint.X)) + (num * (pointToRotate.Y - centerPoint.Y))) + centerPoint.Y)
            };
        }

        private static SharpDX.Vector2 RotatePoint(SharpDX.Vector2 pointToRotate, SharpDX.Vector2 centerPoint, float angle, bool angleInRadians = false)
        {
            if (!angleInRadians)
            {
                angle = (float) (angle * 0.017453292519943295);
            }
            float num = (float) Math.Cos((double) angle);
            float num2 = (float) Math.Sin((double) angle);
            return (new SharpDX.Vector2((num * (pointToRotate.X - centerPoint.X)) - (num2 * (pointToRotate.Y - centerPoint.Y)), (num2 * (pointToRotate.X - centerPoint.X)) + (num * (pointToRotate.Y - centerPoint.Y))) + centerPoint);
        }

        private void SetWindow(object sender)
        {
            IntPtr ptr;
        Label_0000:
            ptr = IntPtr.Zero;
            ptr = Managed.FindWindow(null, "Rust");
            if (ptr != IntPtr.Zero)
            {
                External_ESP_Base.RECT lpRect = new External_ESP_Base.RECT();
                Managed.GetWindowRect(ptr, out lpRect);
                if (((lpRect.Left < 0) && (lpRect.Top < 0)) && ((lpRect.Right < 0) && (lpRect.Bottom < 0)))
                {
                    this.IsMinimized = true;
                    goto Label_0000;
                }
                this.IsMinimized = false;
                External_ESP_Base.RECT rect2 = new External_ESP_Base.RECT();
                Managed.GetClientRect(ptr, out rect2);
                if ((this.rect.Width != (lpRect.Bottom - lpRect.Top)) && (this.rect.Width != (rect2.Right - rect2.Left)))
                {
                    this.IsResize = true;
                }
                this.rect.Width = lpRect.Right - lpRect.Left;
                this.rect.Height = lpRect.Bottom - lpRect.Top;
                if ((Managed.GetWindowLong(ptr, Managed.GWL_STYLE) & Managed.WS_BORDER) != 0)
                {
                    int num = lpRect.Bottom - lpRect.Top;
                    this.rect.Height = rect2.Bottom - rect2.Top;
                    this.rect.Width = rect2.Right - rect2.Left;
                    int num2 = num - rect2.Bottom;
                    int num3 = ((lpRect.Right - lpRect.Left) - rect2.Right) / 2;
                    num2 -= num3;
                    lpRect.Left += num3;
                    lpRect.Top += num2;
                    this.rect.Left = lpRect.Left;
                    this.rect.Top = lpRect.Top;
                }
                Managed.MoveWindow(this.handle, lpRect.Left, lpRect.Top, this.rect.Width, this.rect.Height, true);
            }
            Thread.Sleep(300);
            goto Label_0000;
        }

        private void Update(object sender)
        {
            while (this.IsGameRun())
            {
                if (this.IsResize)
                {
                    this.device.Resize(new Size2(this.rect.Width, this.rect.Height));
                    this.IsResize = false;
                }
                this.device.BeginDraw();
                this.device.Clear(new Color4(0f, 0f, 0f, 0f));
                if (!this.IsMinimized)
                {
                    this.DrawTextCenter((this.rect.Width / 2) - 0x7d, 5, 250, (int) this.font.FontSize, "[UnKnoWnCheaTs] echelon public", new SharpDX.Color(0xff, 0xd6, 0, 0xff), true);
                    SharpDX.Vector2 screenMid = new SharpDX.Vector2 {
                        X = this.rect.Width - 160,
                        Y = 160f
                    };
                    SharpDX.Color color = new SharpDX.Color {
                        R = Radar.Default.radarPrimaryC.R,
                        G = Radar.Default.radarPrimaryC.G,
                        B = Radar.Default.radarPrimaryC.B,
                        A = (byte) Radar.Default.radarPrimaryA
                    };
                    SharpDX.Color color2 = new SharpDX.Color {
                        R = Radar.Default.radarSecondaryC.R,
                        G = Radar.Default.radarSecondaryC.G,
                        B = Radar.Default.radarSecondaryC.B,
                        A = (byte) Radar.Default.radarSecondaryA
                    };
                    SharpDX.Color color3 = new SharpDX.Color {
                        R = Radar.Default.playerColor.R,
                        G = Radar.Default.playerColor.G,
                        B = Radar.Default.playerColor.B,
                        A = (byte) Radar.Default.playerColorA
                    };
                    SharpDX.Color color4 = new SharpDX.Color {
                        R = Radar.Default.rocksColor.R,
                        G = Radar.Default.rocksColor.G,
                        B = Radar.Default.rocksColor.B,
                        A = (byte) Radar.Default.rocksColorA
                    };
                    SharpDX.Color color5 = new SharpDX.Color {
                        R = Radar.Default.cupboardColor.R,
                        G = Radar.Default.cupboardColor.G,
                        B = Radar.Default.cupboardColor.B,
                        A = (byte) Radar.Default.cupboardColorA
                    };
                    SharpDX.Color color6 = new SharpDX.Color {
                        R = Radar.Default.angryColor.R,
                        G = Radar.Default.angryColor.G,
                        B = Radar.Default.angryColor.B,
                        A = (byte) Radar.Default.angryColorA
                    };
                    SharpDX.Color color7 = new SharpDX.Color {
                        R = Radar.Default.niceColor.R,
                        G = Radar.Default.niceColor.G,
                        B = Radar.Default.niceColor.B,
                        A = (byte) Radar.Default.niceColorA
                    };
                    if (Radar.Default.radarSecondaryT)
                    {
                        this.DrawFillCircle(this.rect.Width - 160, 160, 150, color2);
                    }
                    if (Radar.Default.radarPrimaryT)
                    {
                        this.DrawCircle(this.rect.Width - 160, 160, 150, color);
                        this.DrawLine(this.rect.Width - 310, 160, this.rect.Width - 10, 160, color);
                        this.DrawLine(this.rect.Width - 160, 310, this.rect.Width - 160, 10, color);
                    }
                    if (proxyisAlive)
                    {
                        this.DrawText(10, 5, "proxy is alive", new SharpDX.Color(0xff, 0xff, 0xff, 0xff));
                    }
                    if (playerTable.Count > 0)
                    {
                        if (Radar.Default.drawPlayers)
                        {
                            foreach (ProtoBuf.Entity entity in playerTable.Values.ToList<ProtoBuf.Entity>())
                            {
                                if ((entity.basePlayer != null) && !entity.basePlayer.modelState.sleeping)
                                {
                                    this.DrawPlayer(this.device, localPlayer, entity, screenMid, color3);
                                }
                            }
                        }
                        if (Radar.Default.drawRocks)
                        {
                            foreach (ProtoBuf.Entity entity2 in rockTable.Values.ToList<ProtoBuf.Entity>())
                            {
                                if (entity2.baseEntity != null)
                                {
                                    this.DrawEntity(this.device, localPlayer, entity2, screenMid, color4);
                                }
                            }
                        }
                        if (Radar.Default.drawCupboards)
                        {
                            foreach (ProtoBuf.Entity entity3 in tcTable.Values.ToList<ProtoBuf.Entity>())
                            {
                                if (entity3.baseEntity != null)
                                {
                                    this.DrawEntity(this.device, localPlayer, entity3, screenMid, color5);
                                }
                            }
                        }
                        if (Radar.Default.drawAngry)
                        {
                            foreach (ProtoBuf.Entity entity4 in angryAnimalTable.Values.ToList<ProtoBuf.Entity>())
                            {
                                if (entity4.baseEntity != null)
                                {
                                    this.DrawEntity(this.device, localPlayer, entity4, screenMid, color6);
                                }
                            }
                        }
                        if (Radar.Default.drawNice)
                        {
                            foreach (ProtoBuf.Entity entity5 in niceAnimalTable.Values.ToList<ProtoBuf.Entity>())
                            {
                                if (entity5.baseEntity != null)
                                {
                                    this.DrawEntity(this.device, localPlayer, entity5, screenMid, color7);
                                }
                            }
                        }
                        SharpDX.Vector2 pointToRotate = new SharpDX.Vector2(screenMid.X + 5f, screenMid.Y + 5f);
                        pointToRotate = RotatePoint(pointToRotate, screenMid, localPlayer.baseEntity.rot.y);
                        this.DrawLine((int) screenMid.X, (int) screenMid.Y, (int) pointToRotate.X, (int) pointToRotate.Y, new SharpDX.Color(0, 0xff, 0, 0xff));
                        this.FillEllipse(this.device, new SharpDX.Color(0, 0xff, 0, 0xff), screenMid.X, screenMid.Y, 8f, 8f, true);
                    }
                }
                this.device.EndDraw();
                this.CalculateFrameRate();
            }
            System.Environment.Exit(0);
        }

        private static PointF[] Vector2ToPointF(params SharpDX.Vector2[] vecs2)
        {
            PointF[] tfArray = new PointF[vecs2.Length];
            for (int i = 0; i < vecs2.Length; i++)
            {
                tfArray[i] = new PointF(vecs2[i].X, vecs2[i].Y);
            }
            return tfArray;
        }

        private static SharpDX.Vector2 Vector3ToVector2(SharpDX.Vector3 vec3) => 
            new SharpDX.Vector2(vec3.X, vec3.Y);

        [StructLayout(LayoutKind.Sequential)]
        private struct Player
        {
            public uint Index;
            public UnityEngine.Vector3 vec3Position;
            public UnityEngine.Vector3 vec3ViewAngles;
            public Player(uint index, UnityEngine.Vector3 position, UnityEngine.Vector3 viewAngles)
            {
                this.Index = index;
                this.vec3Position = position;
                this.vec3ViewAngles = viewAngles;
            }
        }
    }
}

