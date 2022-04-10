using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class FormSnake : Form
    {
        bool left = false;
        bool right = true;
        bool down = false;
        bool up = false;
        int score = 0;
        Graphics graph;
        Snake snake = new Snake();
        Random randFood = new Random();
        Food food;

        public FormSnake()
        {
            InitializeComponent();
            food = new Food(randFood);
        }

        private void FormSnake_Paint(object sender, PaintEventArgs e)
        {
            graph = e.Graphics;
            food.drawFood(graph);
            snake.drawSnake(graph);

            for (int i = 0; i < snake.snakeRec.Length; i++)
            {
                if (snake.snakeRec[i].IntersectsWith(food.foodRec))
                {
                    food.foodLocation(randFood);
                }
            }
        }

        private void FormSnake_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && !left)
            {
                left = false;
                right = true;
                down = false;
                up = false;
            }
            if (e.KeyCode == Keys.Left && !right)
            {
                left = true;
                down = false;
                up = false;
                right = false;
            } 
            if (e.KeyCode == Keys.Up && !down)
            {
                left = false;
                right = false;
                down = false;
                up = true;
            }
            if (e.KeyCode == Keys.Down && !up)
            {
                left = false;
                right = false;
                down = true;
                up = false;
            }
                
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (up) snake.moveUp();
            if (down) snake.moveDown();
            if (left) snake.moveLeft();
            if (right) snake.moveRight();
            for (int i = 0; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[i].IntersectsWith(food.foodRec))
                {
                    score += 10;
                    snake.growSnake();
                    food.foodLocation(randFood);
                }
            }
            scoreStrip.Text = score.ToString();
            collision();
            this.Refresh();
        }

        public void collision()
        {
            for (int i = 1; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[0].IntersectsWith(snake.SnakeRec[i]))
                {
                    MoveTimer.Stop();
                    loose();
                }
            }

            if (snake.SnakeRec[0].X < 0 || snake.SnakeRec[0].X >= this.Width-10)
            {
                MoveTimer.Stop();
                loose();
            }

            if (snake.SnakeRec[0].Y < 0 || snake.SnakeRec[0].Y > this.Height-60)
            {
                MoveTimer.Stop();
                loose();
            }


        }

        public void loose()
        {
            
            DialogResult result = (MessageBox.Show("Вы проиграли! Ваш счёт: " + score.ToString() + "\nХотите попробовать ещё раз?", "Проигрыш", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1));
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                left = false;
                right = true;
                down = false;
                up = false;
                MoveTimer.Start();
                snake = new Snake();
                score = 0;
            }
            else
                this.Close();
        }
 

    }
}
