import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { SignupComponent } from './auth/signup/signup.component';
import { RestaurantBookingComponent } from './components/restaurant-booking/restaurant-booking.component';
import { PrivacyPolicyComponent } from './components/privacy-policy/privacy-policy.component';
import { TermsAndConditionsComponent } from './components/terms-and-conditions/terms-and-conditions.component';
import { RestaurantListComponent } from './components/restaurant-list/restaurant-list.component';
import { HomeBodyComponent } from './components/home-body/home-body.component';
import { SearchListComponent } from './components/search-list/search-list.component';
import { RestaurantReviewsSectionComponent } from './components/restaurant-reviews-section/restaurant-reviews-section.component';

const routes: Routes = [
  {
    path: '',
    component: SearchListComponent,
    children: [
      { path: '', component: HomeBodyComponent },
      { path: 'list', component: RestaurantListComponent },
    ]
  },
  { path: 'review/:id', component: RestaurantReviewsSectionComponent },
  { path: 'restaurant/:id', component: RestaurantComponent },
  { path: 'booking', component: RestaurantBookingComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/signup', component: SignupComponent },
  { path: 'terms-and-conditions', component: TermsAndConditionsComponent },
  { path: 'privacy-policy', component: PrivacyPolicyComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }




// RouterModule.forRoot([
//   { path: ':lang', component: LanguageComponent ,
//     children: [
//       { path: 'shop', component: ShopComponent },
//       { path: 'customer/:id', component: CustomerComponent },
//       { path: '**', redirectTo: '/shop', pathMatch: 'full' }
//     ]
//   },
//         { path: '**', redirectTo: '/en', pathMatch: 'full' }
// ])
