using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiCrud_2.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get(string gender = "all",int? top = 0)
        {
            IQueryable<Employee> query = db.Employees;

            gender = gender.ToLower();

            switch (gender)
            {
                case "all":
                    break;
                case "male":
                    break;
                case "female":
                    query = query.Where(x => x.Gender.ToLower() == gender);
                    break;
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        $"{gender} is not a valid gender. Please use all, male or female.");
            }
            if (top > 0)
            {
                query = query.Take(top.Value);
            }

            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

        public HttpResponseMessage Get(int id)
        {
            Employee employee = db.Employees.FirstOrDefault(x => x.ID == id);
            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Id'si {id} olan satır bulunamdı.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        public HttpResponseMessage Post(Employee employee)
        {
            try
            {
                db.Employees.Add(employee)
                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.ID);

                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri ekleme işlemi yapılamadı.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(Employee employee)
        {
            try
            {
                Employee emp = db.Employees.FirstOrDefault(e => e.ID == employee.ID);

                if (emp == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Id : " + employee.ID);
                }
                else
                {
                    emp.Name = employee.Name;
                    emp.SurName = employee.SurName;
                    emp.Salary = employee.Salary;
                    emp.Gender = employee.Gender;

                    if (db.SaveChanges() > 0)
                    {
                        HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, employee);

                        return message;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Güncelleme yapılamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Employee emp = db.Employees.FirstOrDefault(e => e.ID == id);

                if (emp == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Id : " + id);
                }
                else
                {
                    db.Employees.Remove(emp);
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Employee Id : " + id);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Kayıt Silinemedi.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
