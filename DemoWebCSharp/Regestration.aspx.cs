using System;
using System.Drawing.Printing;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using DemoWebCSharp.Utility;

namespace DemoWebCSharp
{
    public partial class Regestration : System.Web.UI.Page
    {
        RegistrationService service = new RegistrationService();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //GetData();
                //ShowAllConcept();

            }
        }
        public void ShowAllConcept()
            {
            string output = "";

            // 1️⃣ Class + Constructor
            SimpleClass simple = new SimpleClass();
            output += "Class & Constructor → " + simple.Message() + "<br/>";

            // 2️⃣ Encapsulation
            EncapsulationExample enc = new EncapsulationExample("Hariom");
            output += "Encapsulation → " + enc.Name + "<br/>";

            // 3️⃣ Inheritance 
            Child child = new Child();
            output += "Inheritance (Parent Method) → " + child.ParentMessage() + "<br/>";
            output += "Inheritance (Child Method) → " + child.ChildMessage() + "<br/>";

            // 4️⃣ Overriding (Runtime Polymorphism)
            Animal animal = new Dog();
            output += "Overriding → " + animal.Speak() + "<br/>";

            // 5️⃣ Overloading (Compile-time Polymorphism)
            Calculator calc = new Calculator();
            output += "Overloading (2 params) → " + calc.Add(5, 3) + "<br/>";
            output += "Overloading (3 params) → " + calc.Add(5, 3, 2) + "<br/>";



            lblStudentInfo.Text = output;
            }



        protected void rblVaccinated_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlUpload.Visible = (rblVaccinated.SelectedValue == "Yes");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;
                bool isVaccinated = (rblVaccinated.SelectedValue == "Yes");

                DateTime dob;
                if (!DateTime.TryParse(txtDOB.Text, out dob))
                {
                    lblMessage.Text = "<span class='text-danger'>Invalid Date of Birth.</span>";
                    return;
                }
                int age = service.CalculateAge(dob);

                if (age < 18)
                {
                    lblMessage.Text = "<span class='text-danger'>Registration Failed! Age must be 18+</span>";
                    return;
                }
                string gender = rbMale.Checked ? "Male" : "Female";

                string result = service.GenerateSummary(firstName, lastName,
                                                        email, gender,
                                                        age, isVaccinated);

                lblMessage.Text = result;
            }
        }
        protected void ValidateGender(object source, ServerValidateEventArgs args)
        {
            args.IsValid = rbMale.Checked || rbFemale.Checked;
        }

        public void GetData()
        {
            int a = 10;
            int sum = a + 5;
            bool check = a > 5;
            var x = 10;
            var y = 20;

            double b = 5.7;
            int converted = (int)b;

            string str = "20";
            int number = Convert.ToInt32(str);

            string output =
                "1.Value of a = " + a + "<br/>" +
                "2.Sum = " + sum + "<br/>" +
                "3.Is 10 > 5 ? " + check + "<br/>" +
                "4.Double value = " + b + "<br/>" +
                "5.After casting to int = " + converted + "<br/>" +
                "6.String value = " + str + "<br/>" +
                "7.After converting to int = " + number;

            var total = x + y;
            output += " <br/>8. x = " + x;
            output += "<br/>9. y = " + y;
            output += "<br/>10.Sum using var = " + total;
            //forloop
            output += "<br/><br/>11.For Loop:<br/>";
            for (int i = 1; i <= 5; i++)
            {
                output += "i = " + i + "<br/>";
            }


            //If-Else
            var num = 10;
            if (num > 5)
            {
                output += "<br/>12.Number is greater than 5<br/>";
            }
            else
            {
                output += "<br/>Number is 5 or smaller";
            }



            //While Loop
            output += "<br/>13.While Loop:<br/>";
            int w = 1;
            while (w <= 3)
            {
                output += "w = " + w + "<br/>";
                w++;
            }

            //Do-While Loop
            output += "<br/>14.Do-While Loop:<br/>";
            int d = 1;
            do
            {
                output += "d = " + d + "<br/>";
                d++;
            }
            while (d <= 3);

            //Break Example
            output += "<br/>15.Break Example:<br/>";
            for (int i = 1; i <= 5; i++)
            {
                if (i == 3)
                    break;
                output += "i = " + i + "<br/>";
            }



            //Continue Example
            output += "<br/>16.Continue Example:<br/>";
            for (int i = 1; i <= 5; i++)
            {
                if (i == 3)
                    continue;
                output += "i = " + i + "<br/>";
            }


            int result = Multiply(4, 3);
            output += "<br/>17.Multiply 4 and 3 = " + result;

       //switch
            output += "<br/>18.Switch Example:";

            int day = 3;

            switch (day)
            {
                case 1:
                    output += "Day is Monday";
                    break;

                case 2:
                    output += "Day is Tuesday";
                    break;

                case 3:
                    output += "Day is Wednesday<br/>";
                    break;

                case 4:
                    output += "Day is Thursday";
                    break;

                case 5:
                    output += "Day is Friday";
                    break;

                default:
                    output += "Weekend<br/>";
                    break;
            }


            lblResult.Text = output;
        }
        public int Multiply(int x, int y)
        {
            return x * y;
        }








    }
}

