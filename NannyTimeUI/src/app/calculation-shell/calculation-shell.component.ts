import { Component, OnInit } from '@angular/core';
import { TimeclockService } from '../timeclock.service';

@Component({
  selector: 'app-calculation-shell',
  templateUrl: './calculation-shell.component.html',
  styleUrls: ['./calculation-shell.component.scss']
})
export class CalculationShellComponent implements OnInit {

  constructor(timeClock: TimeclockService) { }

  ngOnInit(): void {
  }

}
