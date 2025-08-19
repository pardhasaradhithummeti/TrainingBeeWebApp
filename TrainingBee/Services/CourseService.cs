using System.Security.Policy;
using System.Text.Json;
using TrainingBee.Models;

namespace TrainingBee.Services
{
    public class CourseService
    {
        string url = "http://localhost:42553/gateway/Course";
        public async Task CreateCourseAsync(CourseDTO course)
        {
             // Your Web API endpoint for creating a course
            string data = System.Text.Json.JsonSerializer.Serialize(course);
            StringContent Content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsync(url, Content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Course created successfully.");
                    }



                }
            }
        }
        public async Task<CourseDTO> GetCoursesByIdAsync(int id)
        {
            CourseDTO course = null;

            string Newurl = url+ "/"+ id;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(Newurl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        course = JsonSerializer.Deserialize<CourseDTO>(data, options);
                    }
                }
            }
            return course;
        }
        public async Task<List<CourseDTO>> GetCoursesAsync()
        {
            List<CourseDTO> courses = new List<CourseDTO>();


            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        courses = JsonSerializer.Deserialize<List<CourseDTO>>(data, options);
                    }
                }
            }
            return courses;
        }

        public async Task UpdateCourseAsync(int id, CourseDTO Coursedto)
        {
            string Newurl = url + "/" + id;
            string data = JsonSerializer.Serialize(Coursedto);

            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                using (var response = await client.PutAsync(Newurl, content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Course Update Failed: " + response.StatusCode);
                    }
                }
            }
        }
    }
}
