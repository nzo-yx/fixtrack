// Minimal JS: scene progress and account form preview toggles.
const stage=document.querySelector('#snap-stage');
const scenes=[...document.querySelectorAll('[data-scene]')];
const count=document.querySelector('#scene-count');
const progress=document.querySelector('#rail-progress');
const observer=new IntersectionObserver(entries=>{for(const entry of entries){if(entry.isIntersecting){scenes.forEach(s=>s.classList.toggle('active',s===entry.target));const n=Number(entry.target.dataset.scene);count.textContent=String(n).padStart(2,'0')+' / 06';progress.style.height=(n/6*100)+'%';document.querySelector('.rail').style.color=n===6?'#17204a':'#fff';}}},{root:stage,threshold:.55});
scenes.forEach(s=>observer.observe(s));
document.querySelectorAll('[data-tab]').forEach(btn=>btn.addEventListener('click',()=>{const name=btn.dataset.tab;document.querySelectorAll('[data-tab]').forEach(b=>{b.classList.toggle('active',b===btn);b.setAttribute('aria-selected',String(b===btn));});document.querySelector('#login-form').classList.toggle('hidden',name!=='login');document.querySelector('#register-form').classList.toggle('hidden',name!=='register');}));

// The final scene keeps its hero visible until an account action is selected.
const overlay=document.querySelector('#account-overlay');
const closeAccount=document.querySelector('#close-account');
const accountTabs=[...document.querySelectorAll('[data-tab]')];
function showAccount(tab){
  overlay.classList.remove('hidden');
  const button=accountTabs.find(b=>b.dataset.tab===tab);
  button?.click();
  closeAccount.focus();
}
document.querySelectorAll('[data-open-account]').forEach(b=>b.addEventListener('click',()=>showAccount(b.dataset.openAccount)));
closeAccount.addEventListener('click',()=>overlay.classList.add('hidden'));
overlay.addEventListener('click',e=>{if(e.target===overlay)overlay.classList.add('hidden')});
document.addEventListener('keydown',e=>{if(e.key==='Escape'&&!overlay.classList.contains('hidden'))overlay.classList.add('hidden')});
