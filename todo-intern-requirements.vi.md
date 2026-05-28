# Bài thực hành cho Thực tập sinh: Todo Demo (Chỉ yêu cầu)

## Mục tiêu
Xây dựng một ứng dụng Todo chạy được, bao gồm:
- Frontend (Web)
- Backend API
- Cơ sở dữ liệu lưu trữ (persistent)

Không giới hạn công nghệ.

## Phạm vi
Chỉ chức năng Todo. Không cần:
- Đăng ký / đăng nhập / hệ thống người dùng
- Phân quyền / quyền truy cập
- Triển khai (deployment)
- UI phức tạp

## Yêu cầu bắt buộc

### Dữ liệu Todo
- `id`: định danh duy nhất
- `title`: bắt buộc
- `completed`: boolean (trạng thái hoàn thành)

### Backend API
- `POST /todos`: tạo Todo (`title` bắt buộc)
- `GET /todos`: lấy danh sách Todo
- `PATCH /todos/:id`: cập nhật Todo (bắt buộc hỗ trợ cập nhật `completed`; tuỳ chọn hỗ trợ cập nhật `title`)
- `DELETE /todos/:id`: xoá Todo

Quy định:
- `title` không được để trống (validate cơ bản là đủ)
- Todo không tồn tại thì trả 404 (hoặc format lỗi thống nhất)
- Bắt buộc dùng cơ sở dữ liệu thật có lưu trữ lâu dài (không chỉ dùng mảng in-memory)

### Trang Frontend
- Hiển thị danh sách Todo
- Thêm Todo mới
- Bật/tắt hoàn thành (completed / not completed)
- Xoá Todo

## Yêu cầu cơ sở dữ liệu
- Có thể dùng bất kỳ DB nào (SQLite cũng được)
- Dữ liệu phải còn sau khi restart backend
- Cần có cách khởi tạo DB (SQL tạo bảng hoặc migrations)

---