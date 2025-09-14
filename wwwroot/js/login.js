document.querySelectorAll('.tab').forEach(tab => {
    tab.addEventListener('click', () => {
        const tabName = tab.getAttribute('data-tab');

        // Cập nhật active tab
        document.querySelectorAll('.tab').forEach(t => t.classList.remove('active'));
        tab.classList.add('active');

        // Hiển thị form tương ứng
        document.querySelectorAll('.form').forEach(form => form.classList.remove('active'));
        document.getElementById(tabName + '-form').classList.add('active');
    });
});

// Xử lý đăng nhập
function handleSignin(event) {
    event.preventDefault();
    const email = document.getElementById('signin-email').value;
    const password = document.getElementById('signin-password').value;

    if (email && password) {
        // Giả lập quá trình đăng nhập
        const button = event.target;
        button.innerHTML = '<i class="fas fa-spinner fa-spin" style="margin-right: 8px;"></i>Đang xử lý...';
        button.disabled = true;

        setTimeout(() => {
            alert('Đăng nhập thành công!\nChuyển hướng đến trang chủ AgriWiki...');
            button.innerHTML = '<i class="fas fa-sign-in-alt" style="margin-right: 8px;"></i>Đăng Nhập';
            button.disabled = false;
        }, 2000);
    } else {
        alert('Vui lòng nhập đầy đủ thông tin!');
    }
}

// Xử lý đăng ký
function handleSignup(event) {
    event.preventDefault();
    const name = document.getElementById('signup-name').value;
    const email = document.getElementById('signup-email').value;
    const password = document.getElementById('signup-password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

    if (!name || !email || !password || !confirmPassword) {
        alert('Vui lòng điền đầy đủ thông tin!');
        return;
    }

    if (password !== confirmPassword) {
        alert('Mật khẩu xác nhận không khớp!');
        return;
    }

    if (password.length < 6) {
        alert('Mật khẩu phải có ít nhất 6 ký tự!');
        return;
    }

    // Giả lập quá trình đăng ký
    const button = event.target;
    button.innerHTML = '<i class="fas fa-spinner fa-spin" style="margin-right: 8px;"></i>Đang xử lý...';
    button.disabled = true;

    setTimeout(() => {
        alert('Đăng ký thành công!\nVui lòng kiểm tra email để xác thực tài khoản.');
        button.innerHTML = '<i class="fas fa-user-plus" style="margin-right: 8px;"></i>Đăng Ký';
        button.disabled = false;

        // Chuyển về tab đăng nhập
        document.querySelector('.tab[data-tab="signin"]').click();
    }, 2000);
}

// Thêm hiệu ứng focus cho input
document.querySelectorAll('input').forEach(input => {
    input.addEventListener('focus', function () {
        this.parentElement.parentElement.style.transform = 'translateY(-2px)';
    });

    input.addEventListener('blur', function () {
        this.parentElement.parentElement.style.transform = 'translateY(0)';
    });
});

// Thêm hiệu ứng particle nhẹ (tùy chọn)
function createParticle() {
    const particle = document.createElement('div');
    particle.style.cssText = `
                position: fixed;
                width: 4px;
                height: 4px;
                background: rgba(102, 187, 106, 0.25);
                border-radius: 50%;
                pointer-events: none;
                z-index: -1;
                animation: float 6s linear infinite;
            `;

    particle.style.left = Math.random() * window.innerWidth + 'px';
    particle.style.top = window.innerHeight + 'px';

    document.body.appendChild(particle);

    setTimeout(() => {
        particle.remove();
    }, 6000);
}

// Tạo particle mỗi 3 giây
setInterval(createParticle, 3000);

// CSS animation cho particle
const style = document.createElement('style');
style.textContent = `
            @keyframes float {
                0% {
                    transform: translateY(0) rotate(0deg);
                    opacity: 0;
                }
                10% {
                    opacity: 1;
                }
                90% {
                    opacity: 1;
                }
                100% {
                    transform: translateY(-100vh) rotate(360deg);
                    opacity: 0;
                }
            }
        `;
document.head.appendChild(style);