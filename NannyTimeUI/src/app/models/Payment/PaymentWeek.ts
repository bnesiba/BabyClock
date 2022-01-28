export interface PaymentWeek{
    AllTimes: DateTimeSpan[];
    ShareTimes: DateTimeSpan[];

    NormalHours: number; 
    ShareHours: number;
    OverTimeHours: number;
    HadOvertime: boolean;
    
    TotalPayment: number;
    NormalHoursPayment: number;
    ShareHoursPayment: number;
    OvertimePayment: number;
    ShareOvertimePayment: number; 
}

export interface DateTimeSpan{
    Start: Date;
    End: Date;
    Hours: number;
}

export interface PaymentResult{
    ArthurTimes: DateTimeSpan[];
    ShareTimes: DateTimeSpan[];
    
    ArthurCost: number;
    ArthurWeeks: PaymentWeek[];
}