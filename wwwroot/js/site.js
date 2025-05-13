document.addEventListener('DOMContentLoaded', function() {
  const heading = document.querySelector('.typing-bounce-heading');
  if (!heading) return;

  const text = heading.textContent;
  heading.textContent = '';
  
  
  const characters = text.split('');
  
 
  characters.forEach(char => {
    const span = document.createElement('span');
    span.textContent = char === ' ' ? '\u00A0' : char; 
    heading.appendChild(span);
  });
  
 
  const spans = heading.querySelectorAll('span');
  spans.forEach((span, index) => {
    setTimeout(() => {
      span.classList.add('visible');
    }, index * 150); // 
  });
});