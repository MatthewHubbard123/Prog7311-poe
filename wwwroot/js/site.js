document.addEventListener('DOMContentLoaded', function() {
  // Typing bounce animation (keep if you still want this effect)
  const heading = document.querySelector('.typing-bounce-heading');
  if (heading) {
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
      }, index * 150);
    });
  }

   const paragraphs = document.querySelectorAll('.dashboard-content p');
  paragraphs.forEach(paragraph => {
    const text = paragraph.textContent;
    paragraph.textContent = '';
    
    // Split by words
    const words = text.trim().split(/\s+/);
    
    words.forEach((word, index) => {
      const span = document.createElement('span');
      span.className = 'word-container';
      span.textContent = word;
      paragraph.appendChild(span);
      
      // Add space after each word except the last one
      if (index < words.length - 1) {
        paragraph.appendChild(document.createTextNode(' '));
      }
    });
  });
});