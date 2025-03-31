import { Injectable } from '@angular/core';
import {ToastController} from "@ionic/angular";

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private toastController: ToastController) { }


  async showError(message: string) {
    const toast = await this.toastController.create({
      message: message || 'Something went wrong!',
      duration: 5000, // Show for 3 seconds
      position: 'bottom',
      color: 'danger'
    });
    await toast.present();
  }

  async showSuccess(message: string) {
    const toast = await this.toastController.create({
      message: message || 'Operation successful!',
      duration: 5000,
      position: 'bottom',
      color: 'success'
    });
    await toast.present();
  }

}
