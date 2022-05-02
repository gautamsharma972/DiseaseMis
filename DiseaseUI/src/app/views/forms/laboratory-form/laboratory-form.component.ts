import {Component, Input, OnInit} from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-laboratory-form',
  templateUrl: './laboratory-form.component.html',
  styleUrls: ['./laboratory-form.component.css']
})
export class LaboratoryFormComponent implements OnInit {
  @Input() content: any;
  initiateClass: boolean = true;
  loading: boolean = false;
  constructor() {
  }

  ngOnInit(): void {
  }

  calcTotal(subTest: any) {
    var sum = 0;
    var cases = 0;
    $(`.animal_${subTest.id}_total`).each(function () {
      // @ts-ignore
      sum += +$(this).val();
    });
    $(`.animal_${subTest.id}_noOfCases`).each(function () {
      // @ts-ignore
      cases += +$(this).val();
    });

    $(`.animal_${subTest.id}_grandTotal`).val(sum);
    $(`.animal_${subTest.id}_totalNoOfCases`).val(cases);
  }
}
