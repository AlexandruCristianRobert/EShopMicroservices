namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : Grpc.Discount.DiscountBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc"};

            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount: {amount}, Description: {description}", coupon.ProductName, coupon.Amount, coupon.Description);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName : {productName}, Amount: {amount}, Description: {description}", coupon.ProductName, coupon.Amount, coupon.Description);
            
            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }
        public override async Task<CouponModel> UpadeDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated. ProductName : {productName}", coupon.ProductName);
            
            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if(coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName = {request.ProductName} was not found"));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName : {productName}", coupon.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
