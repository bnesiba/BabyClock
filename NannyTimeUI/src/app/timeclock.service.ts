import { Injectable } from '@angular/core';
import { TimeClockState } from './models/TimeClockState';

@Injectable({
  providedIn: 'root'
})
export class TimeclockService {

  constructor() { }

  public GetCurrentState(): TimeClockState{
    console.log("Getting current state");
    return {ArthurClockedIn:false,EmiliaClockedIn:false} as TimeClockState
  }

  public SetState(newState: TimeClockState){
    console.log("Setting current state: ArthurClockedIn-"+newState.ArthurClockedIn+" EmiliaClockedIn-"+newState.EmiliaClockedIn);
    return; 
  }
}
