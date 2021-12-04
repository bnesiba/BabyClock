import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-ui-shell',
  templateUrl: './ui-shell.component.html',
  styleUrls: ['./ui-shell.component.scss']
})
export class UiShellComponent implements OnInit {

  public arthurClockingIn = true;
  public EmiliaClockingIn = true;
  public ArthurText = "Arthur - Clock In";
  public EmiliaText = "Emilia - Clock In";
  constructor() { }

  ngOnInit(): void {
  }

  public ToggleArthur(){
    this.arthurClockingIn = !this.arthurClockingIn;
    if(this.arthurClockingIn){
      this.ArthurText = "Arthur - Clock In"
    }else{
      this.ArthurText = "Arthur - Clock Out"
    }
  }

  public ToggleEmilia(){
    this.EmiliaClockingIn = !this.EmiliaClockingIn;
    if(this.EmiliaClockingIn){
      this.EmiliaText = "Emilia - Clock In"
    }else{
      this.EmiliaText = "Emilia - Clock Out"
    }
  }

}
