import { AfterViewInit, QueryList, ViewChildren } from '@angular/core';
import { ViewChild } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core'; 
declare var $: any;

@Component({
  selector: 'app-disease-form',
  templateUrl: './disease-form.component.html',
  styleUrls: ['./disease-form.component.css']
})
export class DiseaseFormComponent implements OnInit, AfterViewInit {

  @Input() content: any;
  @Input() formType: any;
    
  constructor() { }

  ngAfterViewInit() {
     
  }

  ngOnInit(): void {
  }
   
  calcTotal(symptom: any) {
    var sum = 0;
    $(`.animal_${symptom.id}`).each(function () {
      // @ts-ignore
      sum += +$(this).val();
    });
    $(`.animal_${symptom.id}_total`).val(sum);

  }
}
