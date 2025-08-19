using System.Text.Json;
using TrainingBee.Models;
namespace TrainingBee.Services
{
    public class StudentService
    {
        string url = "http://localhost:42553/gateway/Student";
        public async Task<List<StudentDTO>> GetStudentsAsync()
        {
            List<StudentDTO> students = new List<StudentDTO>();

            

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
                        students = JsonSerializer.Deserialize<List<StudentDTO>>(data, options);
                    }
                }
            }
            return students;
        }

        public async Task<StudentDTO> GetStudentsByRollNoAsync(int rollno)
        {
            StudentDTO student = null;

            string newurl = url + "/" + rollno ;

            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(newurl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        student = JsonSerializer.Deserialize<StudentDTO>(data, options);
                    }else
                    {
                        throw new Exception("Not found");
                    }
                }
            }
            return student;
        }

        public async Task CreateStudent(StudentDTO studentDTO)
        {
            string data = JsonSerializer.Serialize(studentDTO);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsync(url, content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Student creation failed.");
                    }
                }
            }
        }

    }
}
