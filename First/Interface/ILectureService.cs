using First.Database;

namespace First.Interface
{
    public interface ILectureService
    {
        public Task<List<Lecture>> GetLectures();

        public void AddLecture (Lecture lecture);

        public void RemoveLecture (Guid lectureId);

        public void UpdateLecture(Lecture lecture);
    }
}
