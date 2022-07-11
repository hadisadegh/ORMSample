using DataLayer.Models;
using DataLayer.Repositories;
using System;

namespace DataLayer.Serve
{
    public class Core : IDisposable
    {
        private DadashiEnglishContext _context = new DadashiEnglishContext();

        private MainRepo<Category> _Category;
        private MainRepo<Comment> _Comment;
        private MainRepo<Course> _Course;
        private MainRepo<Discount> _Discount;
        private MainRepo<Meeting> _Meeting;
        private MainRepo<Order> _Order;
        private MainRepo<OrderDetail> _OrderDetail;
        private MainRepo<Role> _Role;
        private MainRepo<User> _User;
        private MainRepo<Message> _Message;
        private MainRepo<Blog> _Blog;
        private MainRepo<BlogComment> _BlogComment;

        public MainRepo<Category> Category => _Category ??= new MainRepo<Category>(_context);
        public MainRepo<Comment> Comment => _Comment ??= new MainRepo<Comment>(_context);
        public MainRepo<Course> Course => _Course ??= new MainRepo<Course>(_context);
        public MainRepo<Discount> Discount => _Discount ??= new MainRepo<Discount>(_context);
        public MainRepo<Meeting> Meeting => _Meeting ??= new MainRepo<Meeting>(_context);
        public MainRepo<Order> Order => _Order ??= new MainRepo<Order>(_context);
        public MainRepo<OrderDetail> OrderDetail => _OrderDetail ??= new MainRepo<OrderDetail>(_context);
        public MainRepo<Role> Role => _Role ??= new MainRepo<Role>(_context);
        public MainRepo<User> User => _User ??= new MainRepo<User>(_context);
        public MainRepo<Message> Message => _Message ??= new MainRepo<Message>(_context);
        public MainRepo<Blog> Blog => _Blog ??= new MainRepo<Blog>(_context);
        public MainRepo<BlogComment> BlogComment => _BlogComment ??= new MainRepo<BlogComment>(_context);
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
