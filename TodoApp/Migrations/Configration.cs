namespace TodoApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TodoApp.Models;

    internal sealed class Configration : DbMigrationsConfiguration<TodoApp.Models.TodoesContext>
    {
        public Configration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;//データが削除される変更を許可するか
            ContextKey = "TodoApp.Models.TodoesContext";
        }

        protected override void Seed(TodoApp.Models.TodoesContext context)//マイグレーションされたときに自動で実行される。
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            User admin = new User()
            {
                Id = 1,
                UserName = "admin",
                Password = "password",
                Roles = new List<Role>()
            };

            Role administrators = new Role()
            {
                Id = 1,
                RoleName = "Administrators",
                Users = new List<User>()
            };

            Role users = new Role()
            {
                Id = 2,
                RoleName = "Users",
                Users = new List<User>()
            };

            var membershipProbider = new CustomMembershipProvider();

            admin.Password = membershipProbider.GeneratePasswordHash(admin.UserName, admin.Password);
            admin.Roles.Add(administrators);
            administrators.Users.Add(admin);

            context.Roles.AddOrUpdate(role => role.Id, new Role[] { administrators, users });

        }
    }
}
