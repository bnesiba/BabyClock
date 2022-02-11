export interface PaymentWeek{
    allTimes: DateTimeSpan[];
    shareTimes: DateTimeSpan[];

    normalHours: number; 
    shareHours: number;
    overTimeHours: number;
    hadOvertime: boolean;
    
    totalPayment: number;
    normalHoursPayment: number;
    shareHoursPayment: number;
    overtimePayment: number;
    shareOvertimePayment: number; 
}

export interface DateTimeSpan{
    start: Date;
    end: Date;
    hours: number;
}

export interface PaymentResult{
    arthurTimes: DateTimeSpan[];
    shareTimes: DateTimeSpan[];
    
    arthurCost: number;
    arthurWeeks: PaymentWeek[];
}