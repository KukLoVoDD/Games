using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace творчЗмейка
{
    public partial class Form1 : Form
    {
        Point[] snake;
        Point food;
        int length = 1;
        string direction = "up";
        SolidBrush b;
        SolidBrush snake_brush;
        SolidBrush food_brush;
        Random r;
        int width, height;
        int scoree = 0;
        string score  = "0";

        public Form1()
        {
            r = new Random();
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height); // поле

            snake = new Point[100000000]; // макс длина змейки 
            width = pictureBox1.Width / 15;
            height = pictureBox1.Height / 15;
            snake[0].X = width / 2;
            snake[0].Y = height / 2;

            b = new SolidBrush(Color.White); // цвет поля
            snake_brush = new SolidBrush(Color.Black);
            food_brush = new SolidBrush(Color.Red);

            food.X = r.Next(0, width - 1); // генератор яблока -1 чтобы не выйти за пределы поля
            food.Y = r.Next(0, height - 1);
            timer1.Enabled = true;
           
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.FillRectangle(b, 0, 0, pictureBox1.Width, pictureBox1.Height); // очистка и создание поля

            for (int i = 1; i < length; i++) // самоепоедание
                for (int j = i + 1; j < length; j++)
                {
                    if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                    {
                        textBox1.Enabled = true;
                        textBox1.Text = "Игра закончена\n Вы набрали " + score + " очков";
                    }
                }
            for (int i = 0; i < length; i ++)
            {
                if (snake[i].X < 0) snake[i].X += width;
                if (snake[i].X > width) snake[i].X -= width;
                if (snake[i].Y < 0) snake[i].Y += height;
                if (snake[i].Y > height) snake[i].Y -= height;
                // избавление от выхода за пределы

                g.FillEllipse(snake_brush, snake[i].X*15, snake[i].Y*15,15,15); // размер змейки

                if (food.X == snake[i].X && food.Y == snake[i].Y) // поедание фрукта
                {
                    food.X = r.Next(0, width  - 1); 
                    food.Y = r.Next(0, height - 1);
                    length++;
                    scoree++;
                    if (scoree == 100)
                    {
                        textBox1.Enabled = true;
                        textBox1.Text = "Вы победили, теперь весь мир у ваших ног";
                    }
                    score = Convert.ToString(scoree); // очки за сбор яблок
                    label1.Text = score;
                }
               
            }

            g.FillEllipse(food_brush, food.X*15, food.Y*15, 15,15); // яблоко
            if (direction == "up") snake[0].Y -= 1;
            if (direction == "down") snake[0].Y += 1;
            if (direction == "left") snake[0].X -= 1;
            if (direction == "right") snake[0].X += 1;

            if (length > 100000000 - 2) // бесконечная змейка
            {
                length = 100000000 - 2;
            }

            for (int i = length - 1; i >= 0; i --) // увеличение змейки
            {
                snake[i + 1].X = snake[i].X;
                snake[i + 1].Y = snake[i].Y;
            }

            pictureBox1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // управление 
        { 
            if (e.KeyCode == Keys.Left)
                direction = "left";

            if (e.KeyCode == Keys.Right)
                direction = "right";

            if (e.KeyCode == Keys.Up)
                direction = "up";

            if (e.KeyCode == Keys.Down)
                direction = "down";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
