function toggleMenu(){
    const menu=document.querySelector(".menu-links");
    const icon=document.querySelector(".hamburger-icon");
    menu.classList.toggle("open");
    icon.classList.toggle("open");
}
const roles = ["Web Developer", "Problem Solver", "SEO Expert",
    "Android Developer"];
let idx = 0;
const roleText = document.getElementById("role-text");

setInterval(() => {
  roleText.style.opacity = 0;
  setTimeout(() => {
    roleText.textContent = roles[idx];
    roleText.style.opacity = 1;
    idx = (idx + 1) % roles.length;
  }, 500); // fade-out duration
}, 2500); // change every 2 seconds
