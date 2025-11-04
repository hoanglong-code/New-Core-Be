# New-Core-Be

## Tổng quan
Backend .NET 8 theo mô hình Clean Architecture + CQRS, tích hợp EF Core, Dapper, MinIO, JWT, SignalR. Dự án được chia thành 4 project:

- `Domain`: Khai báo Entities, Enums, Exceptions, Constants, Commons
- `Application`: DTOs, Validations, AutoMapper Profiles, Interfaces (Repository/UnitOfWork), Contexts
- `Infrastructure`: EF Core Configurations, Dapper, Repositories/UnitOfWork, Services (MinIO/Auth/Product), Middleware/Filters, Extensions, SignalR Hubs, Features (CQRS)
- `WebApi`: ASP.NET Core API host (Controllers, Program, appsettings)

## Kiến trúc & công nghệ
- **Nền tảng**: .NET 8, C#
- **Mẫu**: Clean Architecture, Repository + UnitOfWork, CQRS (Commands/Queries)
- **ORM/DB**: EF Core (Configurations), Dapper (truy vấn hiệu năng)
- **Storage**: MinIO (S3-compatible)
- **Auth**: JWT (Bearer)
- **Realtime**: SignalR (Hubs)
- **Khác**: ElasticSearch helper (tuỳ chọn)

## Cấu trúc thư mục (rút gọn)
```text
CleanArchitecture.NET.sln
Domain/
  Entities/, Enums/, Exceptions/, Constants/
Application/
  Contexts/, EntityDtos/, Profiles/, Validations/, IReponsitories/
Infrastructure/
  Configurations/ (EF Core)
  Dapper/
  Reponsitories/
  Services/ (MinIO, Auth, Product)
  Features/
    Products/ { Commands/, Queries/ }
    MinIO/    { Commands/, Queries/ }
  Middlewares/, Filters/, Extensions/, Hubs/
WebApi/
  Controllers/ (ProductController, MinioController)
  Program.cs, appsettings.json
```

## Yêu cầu hệ thống
- .NET SDK 8.0+
- SQL Server (hoặc DB tương thích tuỳ bạn cấu hình `AppDbContext`)
- MinIO (hoặc dịch vụ S3 tương thích)
- (Tuỳ chọn) ElasticSearch nếu sử dụng

## Thiết lập nhanh
1) Khôi phục và build solution
```bash
dotnet restore
dotnet build
```

2) Cấu hình `appsettings.json` cho `WebApi`
Tạo/điền file `WebApi/appsettings.Development.json` (ưu tiên) hoặc cập nhật `appsettings.json` với thông tin bên dưới.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NewCoreDb;User Id=sa;Password=your_password;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "SecretKey": "replace-with-a-strong-secret-key"
  },
  "MinIO": {
    "Endpoint": "http://localhost:9000",
    "AccessKey": "minioadmin",
    "SecretKey": "minioadmin",
    "Bucket": "your-bucket",
    "WithSSL": false
  },
  "ElasticSearch": {
    "Uri": "http://localhost:9200",
    "User": "",
    "Password": ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

3) Chạy database migrations (nếu dùng EF migrations)
- Dự án đã có `AppDbContext` trong `Infrastructure/Configurations/AppDbContext.cs`. Nếu chưa có migrations, bạn có thể tạo:
```bash
cd Infrastructure
dotnet ef migrations add InitialCreate -s ../WebApi -p Infrastructure.csproj
dotnet ef database update -s ../WebApi -p Infrastructure.csproj
cd ..
```
Lưu ý: Cần cài `dotnet-ef` nếu chưa có: `dotnet tool install --global dotnet-ef`.

4) Khởi chạy API
```bash
cd WebApi
dotnet run
```
API mặc định lắng nghe trên `http://localhost:5089` hoặc cổng hiển thị trong console.

## MinIO (Local)
- Chạy MinIO nhanh bằng Docker:
```bash
docker run -d \
  -p 9000:9000 -p 9001:9001 \
  -e MINIO_ROOT_USER=minioadmin \
  -e MINIO_ROOT_PASSWORD=minioadmin \
  --name minio \
  quay.io/minio/minio server /data --console-address ":9001"
```
- Truy cập console: http://localhost:9001, tạo bucket trùng tên trong cấu hình.

## Endpoints chính (tóm tắt)
- `ProductController` (`/api/products`)
  - CRUD sản phẩm (tham khảo mã trong `Infrastructure/Features/Products/{Commands,Queries}`)
- `MinioController` (`/api/minio`)
  - Upload/Download/List đối tượng (tham khảo `Infrastructure/Features/MinIO/{Commands,Queries}`)

Chi tiết model/DTO/mapping: xem `Application/EntityDtos`, `Application/Profiles`, `Infrastructure/CommandProfiles`.

## Middleware, Auth, Validation
- `Infrastructure/Middlewares/UserContextMiddleware.cs`: gắn user context từ JWT
- `Infrastructure/CustomAuthorize/CustomAuthorizeAttribute.cs`: kiểm soát truy cập theo role/claims
- `Application/Validations`: kiểm tra dữ liệu đầu vào (Fluent-like)

## SignalR
- Hubs trong `Infrastructure/Hubs` (client interface + implementation). Cấu hình hub trong `WebApi/Program.cs` (nếu đã map hubs).

## Quy ước phát triển
- Sử dụng CQRS: Commands cho tác vụ ghi, Queries cho đọc
- Tầng `Domain` thuần POCO, không phụ thuộc infra
- Tầng `Application` chứa DTO/Validation/Mapping/Contracts, không truy cập trực tiếp DB
- Tầng `Infrastructure` thực thi persistence, services, middleware, filters
- Tầng `WebApi` chỉ chứa wiring và controllers

## Troubleshooting
- Lỗi kết nối DB: kiểm tra `ConnectionStrings:DefaultConnection`
- 401/403: kiểm tra JWT `Issuer/Audience/SecretKey` và header `Authorization: Bearer <token>`
- MinIO: xác nhận `Endpoint`, `AccessKey/SecretKey`, `Bucket` tồn tại
- EF Tools: đảm bảo đã cài `dotnet-ef` và chọn đúng `-s` (startup `WebApi`) và `-p` (project `Infrastructure`)

## License
Private/Proprietary (cập nhật theo nhu cầu dự án).
