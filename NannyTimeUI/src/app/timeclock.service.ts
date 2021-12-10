import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TimeClockState } from './models/TimeClockState';

@Injectable({
  providedIn: 'root'
})
export class TimeclockService {

  private baseaddress = "https://localhost:44360/api/Timeclock"
  private currentState: TimeClockState = {} as TimeClockState;
  constructor(private http: HttpClient) { }

  public GetCurrentState(): Observable<TimeClockState>{
    console.log("Getting current state");
    return this.http.get<TimeClockState>(this.baseaddress + "/GetState")
  }

  public SetState(newState: TimeClockState){
    console.log("Setting current state: ArthurClockedIn-"+newState.arthurClockedIn+" EmiliaClockedIn-"+newState.emiliaClockedIn);
    var result = this.http.post(this.baseaddress+"/SetState",newState).subscribe();
    return result; 
  }
}
