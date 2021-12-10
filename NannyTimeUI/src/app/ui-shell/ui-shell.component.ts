import { Component, OnInit } from '@angular/core';
import { TimeClockState } from '../models/TimeClockState';
import { TimeclockService } from '../timeclock.service';

@Component({
  selector: 'app-ui-shell',
  templateUrl: './ui-shell.component.html',
  styleUrls: ['./ui-shell.component.scss']
})
export class UiShellComponent implements OnInit {

  private  arthurClockInText =  "Arthur - Clock In";
  private  emiliaClockInText = "Emilia - Clock In";
  private  arthurClockOutText =  "Arthur - Clock Out";
  private  emiliaClockOutText = "Emilia - Clock Out";
  public timeClockState : TimeClockState = {} as TimeClockState

  public ArthurText = this.arthurClockInText;
  public EmiliaText = this.emiliaClockInText;

  constructor(private timeclock: TimeclockService) { }

  ngOnInit(): void {
    this.timeclock.GetCurrentState().subscribe((result) => {
      this.timeClockState.arthurClockedIn = result.arthurClockedIn;
      this.timeClockState.emiliaClockedIn = result.emiliaClockedIn;
      this.SetArthurText();
      this.SetEmiliaText();
    });
  }

  public ToggleArthur(){
    if(this.timeClockState){
      this.timeClockState.arthurClockedIn = !this.timeClockState.arthurClockedIn;
      this.timeclock.SetState(this.timeClockState);
    }
    this.SetArthurText();
    if(this.timeClockState){
      
    }
  }

  public ToggleEmilia(){
    if(this.timeClockState){
      this.timeClockState.emiliaClockedIn = !this.timeClockState.emiliaClockedIn;
      this.timeclock.SetState(this.timeClockState);
    }
    this.SetEmiliaText();
  }

  public ArthurClockedIn(){
    if(this.timeClockState && this.timeClockState.arthurClockedIn){
      return this.timeClockState.arthurClockedIn;
    }else{
      return false;
    }
  }

  public EmiliaClockedin(){
    if(this.timeClockState && this.timeClockState.emiliaClockedIn){
      return this.timeClockState.emiliaClockedIn;
    }else{
      return false;
    }
  }

  private SetArthurText(){
    if(!this.timeClockState?.arthurClockedIn){
      this.ArthurText = this.arthurClockInText;
    }else{
      this.ArthurText = this.arthurClockOutText;
    }
  }

  private SetEmiliaText(){
    if(!this.timeClockState?.emiliaClockedIn){
      this.EmiliaText = this.emiliaClockInText;
    }else{
      this.EmiliaText = this.emiliaClockOutText;
    }
  }

}
