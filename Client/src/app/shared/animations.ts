import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

export const fadeAnimation = trigger('fade', [
  state('void', style({ opacity: 0 })),
  transition(':enter, :leave', [
    animate(500)
  ]),
]);
export const basicAnimation = trigger('anim', [
  state('void', style({ opacity: 0, transform: 'scale(0.95)' })),
  state('*', style({ opacity: 1, transform: 'scale(1)' })),
  transition('void => *', [animate('.5s')]),
]);

export const imageAnimation =
  trigger('animImg', [
    state('void, reset', style({ opacity: 0, transform: 'scale(0.95)' })),
    state('enter', style({ opacity: 1, transform: 'scale(1)' })),
    transition('* => enter', [animate('.5s')]),
  ]);

export const fadeOutAnimation =
trigger('fadeOut', [
  state('visible', style({ opacity: 1 })),
  state('invisible', style({ opacity: 0 })),
  transition('visible => invisible', [animate('0.5s')])
]);
