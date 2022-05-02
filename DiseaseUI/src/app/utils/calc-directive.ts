import { Directive, Input} from '@angular/core';

@Directive({
  selector: '[ngInit]',
  exportAs: 'ngInit'
})
export class CalcDirective {

  // @ts-ignore
  @Input() isLast: any;

  @Input() ngInit: any;
  //@Output('ngInit') initEvent: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    if (this.ngInit) {
      this.ngInit();
    }
  }
}
