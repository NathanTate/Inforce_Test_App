import { Directive, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';

@Directive({
  selector: '[copyToClipboard]'
})
export class CopyDirective {
  @Input('copyToClipboard') data: string = '';
  @Output() onCopied = new EventEmitter<void>();

  @HostListener('pointerdown', ['$event'])
    public onClick(event: PointerEvent): void {
      event.preventDefault();

      const listener = (e: ClipboardEvent) => {
        e.clipboardData?.setData('text', this.data);
        e.preventDefault();

        this.onCopied.emit();
      }

      document.addEventListener('copy', listener, false);
      document.execCommand('copy');
      document.removeEventListener('copy', listener, false);
    }
}
