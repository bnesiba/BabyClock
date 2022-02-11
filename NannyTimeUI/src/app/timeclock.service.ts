import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { PaymentResult } from './models/Payment/PaymentWeek';
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

  public GetPaymentInfo(startDate: Date, endDate: Date): Observable<PaymentResult>{
    console.log("Getting payment info");
    return this.http.get<PaymentResult>(this.baseaddress + `/GetPaymentInfo?startDate=${startDate.toDateString()}&endDate=${endDate.toDateString()}`).pipe(
      map(this.extractData),
      catchError(this.handleErrorObservable)
    )
  }

  private extractData(res: any){
    let body = res;
    return body; 
  }

  private handleErrorObservable(error: any) {
    console.error(error.message || error);
    return throwError(()=> error);
  } 
}
