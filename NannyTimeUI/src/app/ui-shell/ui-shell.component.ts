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
  public timeClockState : TimeClockState | undefined;

  public ArthurText = this.arthurClockInText;
  public EmiliaText = this.emiliaClockInText;

  constructor(private timeclock: TimeclockService) { }

  ngOnInit(): void {
    this.timeClockState = this.timeclock.GetCurrentState();
  }

  public ToggleArthur(){
    if(this.timeClockState){
      this.timeClockState.ArthurClockedIn = !this.timeClockState.ArthurClockedIn;
      this.timeclock.SetState(this.timeClockState);
    }
    this.SetArthurText();
    if(this.timeClockState){
      
    }
  }

  public ToggleEmilia(){
    if(this.timeClockState){
      this.timeClockState.EmiliaClockedIn = !this.timeClockState.EmiliaClockedIn;
      this.timeclock.SetState(this.timeClockState);
    }
    this.SetEmiliaText();
  }

  public ArthurClockedIn(){
    if(this.timeClockState && this.timeClockState.ArthurClockedIn){
      return this.timeClockState.ArthurClockedIn;
    }else{
      return false;
    }
  }

  public EmiliaClockedin(){
    if(this.timeClockState && this.timeClockState.EmiliaClockedIn){
      return this.timeClockState.EmiliaClockedIn;
    }else{
      return false;
    }
  }

  private SetArthurText(){
    if(!this.timeClockState?.ArthurClockedIn){
      this.ArthurText = this.arthurClockInText;
    }else{
      this.ArthurText = this.arthurClockOutText;
    }
  }

  private SetEmiliaText(){
    if(!this.timeClockState?.EmiliaClockedIn){
      this.EmiliaText = this.emiliaClockInText;
    }else{
      this.EmiliaText = this.emiliaClockOutText;
    }
  }

}
