using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPass.Models;

namespace TPass.Api {
//cool
    public interface IK12Api {

        Task<bool> ValidateCredentials(string credentials);
        Task<IEnumerable<AssociatedContact>> GetAssociatedContacts(int ccode);
        Task<IEnumerable<DropOffType>> GetDropOffTypes();
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<StudentBehavior>> GetStudentBehavior(int ccode, DateTime date);
        
        Task<IEnumerable<StudentContact>> GetStudentContacts(int ccode);
        Task<IEnumerable<StudentDetails>> GetStudentDetails(int compid, string id);
        Task<StudentMedicalRecord> GetStudentMedicalRecord(int ccode);
        Task<CheckInRec> GetStudentCheckinRecord(int ccode, DateTime date);
        Task<IEnumerable<StudentSchedule>> GetStudentSchedule(int ccode);
        Task<IEnumerable<TardyType>> GetTardyTypes();
        Task<IEnumerable<BehaviorType>> GetBehaviorTypes();
        Task<StudentLog> GetLatestStudentLog(int compid, int ccode, DateTime date);

        //event based called
        //Task<IEnumerable<EventRecord>> GetEvents(int compid, DateTime date);
        //Task<IEnumerable<EventAttendeeRec>> LoadEventAttendee(int eventid, DateTime date);
        //Task<SignedEventAttendeeRec> LoadCurrentEventCheckIn(int ccode, int eventcode, DateTime date);
        //Task<string> CheckInEventAttendee();
        //Task<string> CheckOutEventAttendee();


    }



}