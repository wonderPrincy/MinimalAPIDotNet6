using First.Database;
using First.Interface;
using Microsoft.EntityFrameworkCore;

namespace First.Services
{
    public class LectureService : ILectureService
    {
        public readonly OnlineTeachingContext _onlineTeachingContext;
        public LectureService(OnlineTeachingContext onlineTeachingContext)
        {
            _onlineTeachingContext = onlineTeachingContext;
        }

        public async void AddLecture(Lecture lecture)
        {
            await _onlineTeachingContext.Lectures.AddAsync(lecture).ConfigureAwait(false);
            await _onlineTeachingContext.SaveChangesAsync();
        }

        public async Task<List<Lecture>> GetLectures()
        {
            return await _onlineTeachingContext.Lectures.AsNoTracking().ToListAsync();
        }

        public async void RemoveLecture(Guid lectureId)
        {
            var lecture = _onlineTeachingContext.Lectures.Where(x => x.Id == lectureId).FirstOrDefault();
            if (lecture != null)
                _onlineTeachingContext.Lectures.Remove(lecture);
            await _onlineTeachingContext.SaveChangesAsync();
        }

        public async void UpdateLecture(Lecture lecture)
        {
            _onlineTeachingContext.Lectures.Update(lecture);
            await _onlineTeachingContext.SaveChangesAsync();
        }
    }
}
