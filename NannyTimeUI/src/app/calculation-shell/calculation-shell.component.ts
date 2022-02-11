import { Component, Input, OnInit } from '@angular/core';
import { PaymentResult, PaymentWeek,DateTimeSpan } from '../models/Payment/PaymentWeek';
import { TimeclockService } from '../timeclock.service';

@Component({
  selector: 'app-calculation-shell',
  templateUrl: './calculation-shell.component.html',
  styleUrls: ['./calculation-shell.component.scss']
})
export class CalculationShellComponent implements OnInit {

  constructor(private timeClock: TimeclockService) { }
  public calcResult!: PaymentResult;
  public loaded = false;

  ngOnInit(): void {
    this.timeClock.GetPaymentInfo(new Date('01/24/2022'), new Date('01/29/2022'))
      .subscribe(paymentResult => {this.calcResult = paymentResult; this.loaded = true; });
      
  }

  public GetCalcData():PaymentResult{
    if(this.calcResult){
      return this.calcResult;
    }else{
      return  {
        arthurTimes: [],
        shareTimes : [],
        arthurCost : 0,
        arthurWeeks : []
      }
    }
  }

  public JSONIFY(thing: any):string {
    return JSON.stringify(thing);
  }

  
  }
  

