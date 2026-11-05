// Cinematic scene observer; auth panels post to real MVC actions.
const stage = document.querySelector('#snap-stage');
const scenes = [...document.querySelectorAll('[data-scene]')];
const count = document.querySelector('#scene-count');
const progress = document.querySelector('#rail-progress');
if (stage && scenes.length && 'IntersectionObserver' in window) {
  const observer = new IntersectionObserver(entries => {
    for (const entry of entries) {
      if (!entry.isIntersecting) continue;
      scenes.forEach(scene => scene.classList.toggle('active', scene === entry.target));
      const n = Number(entry.target.dataset.scene);
      if (count) count.textContent = String(n).padStart(2, '0') + ' / ' + String(scenes.length).padStart(2, '0');
      if (progress) progress.style.height = (n / scenes.length * 100) + '%';
      const rail = document.querySelector('.rail');
      if (rail) rail.style.color = n === scenes.length ? '#fff' : '#fff';
    }
  }, {root: stage, threshold: 0.55});
  scenes.forEach(scene => observer.observe(scene));
}
const overlay = document.querySelector('#account-overlay');
const closeAccount = document.querySelector('#close-account');
const tabs = [...document.querySelectorAll('[data-tab]')];
let previouslyFocused = null;
function selectTab(name) {
  tabs.forEach(tab => {
    const active = tab.dataset.tab === name;
    tab.classList.toggle('active', active);
    tab.setAttribute('aria-selected', String(active));
  });
  document.querySelector('#login-form')?.classList.toggle('hidden', name !== 'login');
  document.querySelector('#register-form')?.classList.toggle('hidden', name !== 'register');
}
function dismiss() {
  if (!overlay) return;
  overlay.classList.add('hidden');
  overlay.setAttribute('aria-hidden', 'true');
  previouslyFocused?.focus();
}
if (overlay && closeAccount) {
  tabs.forEach(tab => tab.addEventListener('click', () => selectTab(tab.dataset.tab)));
  document.querySelectorAll('[data-open-account]').forEach(button => button.addEventListener('click', () => {
    previouslyFocused = button;
    selectTab(button.dataset.openAccount);
    overlay.classList.remove('hidden');
    overlay.setAttribute('aria-hidden', 'false');
    closeAccount.focus();
  }));
  closeAccount.addEventListener('click', dismiss);
  overlay.addEventListener('click', event => { if (event.target === overlay) dismiss(); });
  document.addEventListener('keydown', event => { if (event.key === 'Escape' && !overlay.classList.contains('hidden')) dismiss(); });
}
