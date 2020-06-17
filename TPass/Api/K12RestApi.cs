using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TPass.Models;
using Xamarin.Forms;
//using Plugin.Settings;
//using Plugin.Settings.Abstractions;
using Xamarin.Essentials;
namespace TPass.Api
{
    
    public class K12RestApi : IK12Api
    {

       
        //TODO: should be a config setting
        public static readonly string serviceAddress = "http://173.220.177.75:9034/TPASSMobileService/K12Service.svc";
        //public static readonly bool IsDev = true;
        //public static readonly string serviceAddress = "https://tpass.dmps.k12.ia.us/TPASSMobileService/K12Service.svc";
        public static readonly bool IsDev = false;

        HttpClient client;

        //No idea what this was for
       // static ISettings AppSettings => CrossSettings.Current;

       /* public static string TPassServer {
            get => AppSettings.GetValueOrDefault(nameof(TPassServer), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(TPassServer), value);
        }
        */



        public async Task<IEnumerable<StudentDetails>> GetStudentDetails(
                int compid,
                string id)

        {

            var company = GetCurrentCompany();
            compid = company.CompID;

            var urlFrag = $"{serviceAddress}/GetStudentDetails/{compid}/{id}";

            var json = await this.GetAsString(urlFrag);

            var sd = JsonConvert.DeserializeObject<IEnumerable<StudentDetails>>(json);

            return sd;

        }

        public async Task<IEnumerable<BehaviorType>> GetBehaviorTypes()
        {

            IEnumerable<BehaviorType> bt = GetCacheBehaviorTypes();

            if (bt != null)
                return bt;

            var url = $"{serviceAddress}/GetBehaviorTypes/";
            var json = await this.GetAsString(url);

            bt = JsonConvert.DeserializeObject<IEnumerable<BehaviorType>>(json);
            SetCache("behaviorTypes", bt);
            return bt;
        }


        IEnumerable<BehaviorType> GetCacheBehaviorTypes()
        {
            var bt = "behaviorTypes";
            if (!Application.Current.Properties.ContainsKey(bt))
                return null;

            return Application.Current.Properties[bt] as IEnumerable<BehaviorType>;
        }

        void SetCache(
            string name,
            object thing)
        {
            Application.Current.Properties[name] = thing;
        }

        public async Task<IEnumerable<StudentBehavior>> GetStudentBehavior(
                int ccode,
                DateTime date)
        {

            var dts = date.ToString("yyyy-MM-dd");
            var url = $"{serviceAddress}/GetStudentBehavior/{ccode}/{dts}";
            var json = await this.GetAsString(url);
            var sd = JsonConvert.DeserializeObject<IEnumerable<StudentBehavior>>(json);

            return sd;
        }



        public async Task<StudentLog> GetLatestStudentLog(
                int compid,
                int ccode,
                DateTime date)
        {

            var studentLog = new StudentLog();

            try
            {

                var company = GetCurrentCompany();
                compid = company.CompID;

                var dts = date.ToString("yyyy-MM-dd");
                var url = $"{serviceAddress}/GetLatestStudentLog/{compid}/{ccode}/{dts}";
                var json = await this.GetAsString(url);
                studentLog = JsonConvert.DeserializeObject<StudentLog>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                var p = ex.Message;
            }

            return studentLog;
        }


        public async Task<IEnumerable<Visitor>> GetCurrentVisits(
                int compid,
                DateTime date)

        {
			
            IEnumerable<Visitor> visitors = null;
            try
            {

                var company = GetCurrentCompany();
                compid = company.CompID;

                var dts = date.ToString("MM-dd-yyyy");
                var url = $"{serviceAddress}/GetCurrentVisits/{compid}/{dts}";

                var json = await this.GetAsString(url);

                visitors = JsonConvert.DeserializeObject<IEnumerable<Visitor>>(json);
            }
            catch (Exception ex)
            {
                var p = ex.Message;
            }

            return visitors;
        }

        public async Task<IEnumerable<Visitor>> GetScheduledVisits(
                int compid,
                DateTime date)

        {
            IEnumerable<Visitor> visitors = null;
            try
            {

                var company = GetCurrentCompany();
                compid = company.CompID;

                var dts = date.ToString("MM-dd-yyyy");
                var url = $"{serviceAddress}/GetScheduledVisits/{compid}/{dts}";
                var json = await this.GetAsString(url);

                visitors = JsonConvert.DeserializeObject<IEnumerable<Visitor>>(json);
            }
            catch (Exception ex)
            {
                var p = ex.Message;
            }

            return visitors;
        }

        public async Task<IEnumerable<Visitor>> GetCompletedVisits(
            int compid, 
            DateTime date)

        {

            IEnumerable<Visitor> visitors = null;

            try
            {

                var company = GetCurrentCompany();
                compid = company.CompID;

                var dts = date.ToString("MM-dd-yyyy");
                var url = $"{serviceAddress}/GetCompletedVisits/{compid}/{dts}";
                var json = await this.GetAsString(url);

                visitors = JsonConvert.DeserializeObject<IEnumerable<Visitor>>(json);
            }
            catch (Exception ex)
            {
                var p = ex.Message;
            }

            return visitors;
        }


        public async Task<CheckInRec> GetStudentCheckinRecord(
            int ccode, 
            DateTime date)

        {
            CheckInRec checkinRec = null;

            try
            {
                var dts = date.ToString("MM-dd-yyyy");
                var url = $"{serviceAddress}/GetStudentCheckinRecord/{ccode}/{dts}";
                var json = await this.GetAsString(url);
                checkinRec = JsonConvert.DeserializeObject<CheckInRec>(json);
            }
            catch (Exception ex)
            {
                var p = ex.Message;
            }

            return checkinRec;
        }


        public async Task<IEnumerable<EventRecord>> GetEvents(int compid, DateTime date)
        {
            IEnumerable<EventRecord> eventRec = null;

            var company = GetCurrentCompany();
            compid = company.CompID;

            var dts = date.ToString("MM-dd-yyyy");
            try { 

                var url = $"{serviceAddress}/GetEvents/{compid}/{dts}";
                var json = await GetAsString(url);                        
                eventRec = JsonConvert.DeserializeObject<IEnumerable<EventRecord>>(json);
            }
            catch (Exception ex)
            {
                eventRec = JsonConvert.DeserializeObject<IEnumerable<EventRecord>>("[]");
                string msg = ex.Message;
                //throw;
            }

            return eventRec;

        }

        //new thangsa
        public async Task<IEnumerable<EventAttendeeRec>> LoadEventAttendee(int eventid, DateTime date)
        {

            IEnumerable<EventAttendeeRec> attendees = null;

            var dts = date.ToString("MM-dd-yyyy");
            var url = $"{serviceAddress}/LoadEventAttendee/{eventid}/{dts}";
            var json = await GetAsString(url);
            try
            {
                attendees = JsonConvert.DeserializeObject<IEnumerable<EventAttendeeRec>>(json);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

            return attendees;
        }


        public async Task<SignedEventAttendeeRec> LoadCurrentEventCheckIn(int ccode, int eventid, DateTime date)
        {
            SignedEventAttendeeRec signedAttendeeRec = null;

            var dts = date.ToString("MM-dd-yyyy");
            var url = $"{serviceAddress}/LoadCurrentEventCheckIn/{eventid}/{ccode}/{dts}";
           
            try
            {
                var json = await GetAsString(url);
                signedAttendeeRec = JsonConvert.DeserializeObject<SignedEventAttendeeRec>(json);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

            return signedAttendeeRec;

        }


        public async Task<IEnumerable<StudentContact>> GetStudentContacts(
                int ccode)

        {
            var url = $"{serviceAddress}/GetStudentContacts/{ccode}";
            var json = await this.GetAsString(url);
            return JsonConvert.DeserializeObject<IEnumerable<StudentContact>>(json);

        }

        public async Task<StudentMedicalRecord> GetStudentMedicalRecord(
                int ccode)
        {
            StudentMedicalRecord medicalRecord = null;

            try
            {
                var url = $"{serviceAddress}/GetStudentMedicalRecord/{ccode}";
                var json = await this.GetAsString(url);
                medicalRecord = JsonConvert.DeserializeObject<StudentMedicalRecord>(json);
            }
            catch (Exception ex)
            {
                var oops = ex.Message;
            }

            return medicalRecord;
        }

        public async Task<IEnumerable<StudentSchedule>> GetStudentSchedule(
            int ccode)

        {
            var url = $"{serviceAddress}/GetStudentSchedule/{ccode}";
            IEnumerable<StudentSchedule> schedule = null;

            try
            {
                var json = await this.GetAsString(url);
                              schedule = JsonConvert.DeserializeObject<IEnumerable<StudentSchedule>>(json);
            }
            catch (Exception ex)
            {

                var p = ex.Message;
            }
            return schedule;
        }

       
        

        public async Task<IEnumerable<DropOffType>> GetDropOffTypes()
        {
            var url = $"{serviceAddress}/GetDropOffTypes";
            var json = await this.GetAsString(url);
            return JsonConvert.DeserializeObject<IEnumerable<DropOffType>>(json);

        }

        public async Task<IEnumerable<TardyType>> GetTardyTypes()
        {
            var url = $"{serviceAddress}/GetTardyTypes/";
            var json = await this.GetAsString(url);
            return JsonConvert.DeserializeObject<IEnumerable<TardyType>>(json);

        }

        public async Task<IEnumerable<AssociatedContact>> GetAssociatedContacts(
            int ccode)

        {
            var url = $"{serviceAddress}/GetAssociatedContacts/{ccode}";
            var json = await this.GetAsString(url);
            return JsonConvert.DeserializeObject<IEnumerable<AssociatedContact>>(json);

        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var url = $"{serviceAddress}/GetCompanies/";
            var json = await this.GetAsString(url);
            return JsonConvert.DeserializeObject<IEnumerable<Company>>(json);

        }


        public async Task<string> GetAsString(
            string url)

        {
            string results = String.Empty;

            try
            {

                SetAuthHeader();
                var ret = await client.GetAsync(url);
                ret.EnsureSuccessStatusCode();

                results = await ret.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                var p = ex.Message;
                throw;
            }

            return results;
        }


        string GetAuthToken()
        {

            return Application.Current.Properties["auth"].ToString();
        }

        public void SetAuthToken(string token)
        {
            Application.Current.Properties["auth"] = token;
        }

        public Company GetCurrentCompany()
        {
            return Application.Current.Properties?["company"] as Company;
        }

        public void SetCurrentCompany(Company c)
        {

            Application.Current.Properties["company"] = c;
        }

        public void SetCurrentBehavior(BehaviorType btype)
        {
            Application.Current.Properties["behavior"] = btype;
        }

        public BehaviorType GetCurrentBehaviorType()
        {
            return Application.Current.Properties?["behavior"] as BehaviorType;
        }

        void SetAuthHeader()
        {
            string creds = GetAuthToken();
            if (!string.IsNullOrEmpty(creds))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", creds);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<string> PostAsString(
                Uri url, 
                string json)

        {

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to check in student");

            }

            return await response.Content.ReadAsStringAsync();

        }


        public async Task<string> AddStudentBehavior(
            StudentBehavior behavior)
        {
            var url = new Uri($"{serviceAddress}/AddStudentBehavior");
            var json = WrapJsonWithRec(JsonConvert.SerializeObject(behavior));
            var res = await this.PostAsString(url, json);
            return res;
        }

        string WrapJsonWithRec(string json)
        {
            return "{\"rec\":" + json + "}";
        }

        public async Task<string> CheckInStudent(
                CheckInRec checkinRecord)
        {

            var url = new Uri($"{serviceAddress}/CheckInStudent");
            var json = JsonConvert.SerializeObject(checkinRecord);
            return await this.PostAsString(url, json);

        }


        public async Task<string> CheckOutStudent(
                CheckInRec checkinRec)
        {
            var url = new Uri($"{serviceAddress}/CheckOutStudent");
            var json = JsonConvert.SerializeObject(checkinRec);
            return  await this.PostAsString(url, json);

        }


        public async Task<string> CheckInEventAttendee(SignedEventAttendeeRec attendee)
        {

            var checkInRec = new EventCheckInRec();

            checkInRec.PkID = attendee.PkID.ToString();
            checkInRec.CCode = attendee.CCode.ToString();
            checkInRec.EvntID = attendee.EvntID.ToString();
            checkInRec.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

           
            var url = new Uri($"{serviceAddress}/EventCheckIn");
            var json = WrapJsonWithRec(JsonConvert.SerializeObject(checkInRec));
            var res = await this.PostAsString(url, json);
            return res;
            //throw new NotImplementedException();
        }

        public async Task<string> CheckOutEventAttendee(SignedEventAttendeeRec attendee)
        {
            var checkInRec = new EventCheckInRec();

            checkInRec.PkID = attendee.PkID.ToString();
            checkInRec.CCode = attendee.CCode.ToString();
            checkInRec.EvntID = ""; // attendee.EvntID.ToString();
            checkInRec.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


            var url = new Uri($"{serviceAddress}/EventCheckOut");
            var json = WrapJsonWithRec(JsonConvert.SerializeObject(checkInRec));
            var res = await this.PostAsString(url, json);
            return res;
        }

        public async Task<string> GetAsString(
            Uri url)
        {
            var ret = await client.GetAsync(url);
            ret.EnsureSuccessStatusCode();

            return await ret.Content.ReadAsStringAsync();

        }

        public async Task<bool> ValidateCredentials(
            string credentials)
        {

            bool isValid = false;

           // try
            //{
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = $"{serviceAddress}/TestCredential/";
                var res = await this.GetAsString(url);

                if (!String.IsNullOrEmpty(res))
                {
                    bool.TryParse(res, out isValid);
                }

         //   }
/*            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
                //return false;
            }
            */
            return isValid;
        }

        public string PrepareCredentials(
            string user, 
            string password)

        {
            var authData = string.Format("{0}:{1}", user, password);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
        }


        public K12RestApi()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(serviceAddress);
        }



     



    }

}
