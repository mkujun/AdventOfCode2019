using System;
using System.Collections;
using System.Collections.Generic;

namespace Day6
{
    class Program
    {
        public class TreeNode<T>
        {
            public T Data { get; set; }
            public TreeNode<T> Parent { get; set; }
            public List<TreeNode<T>> Children { get; set; }

            public TreeNode(T data)
            {
                Data = data;
                Children = new List<TreeNode<T>>();
            }

            // find parent, then add child
            public TreeNode<T> AddChild(T child)
            {
                TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
                Children.Add(childNode);
                return childNode;
            }

            // todo: finish this!!
            public TreeNode<T> FindParent(T parent)
            {
                return null;
            }
        }

        static void Main(string[] args)
        {
            TreeNode<int> root = new TreeNode<int>(0);
            {
                TreeNode<int> child1 = root.AddChild(1);
                TreeNode<int> child2 = root.AddChild(2);
                {
                    TreeNode<int> child21 = child2.AddChild(21);
                    TreeNode<int> child22 = child2.AddChild(22);
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}
