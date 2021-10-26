using System;

namespace buckstore.orders.service.application.DTOs
{
   public class OrderResponseDto
      {
          public Guid Id { get; set; }
          public int OrderStatusId { get; set; }
          public string OrderStatus { get; set; }
          public decimal OrderAmount { get; set; }
          public DateTime OrderDate { get; set; }
          // talvez adicionar endereço para cada ordem
  
          public OrderResponseDto(){ }
          public OrderResponseDto(Guid id, int orderStatusId, string orderStatus, decimal orderAmount, DateTime orderDate)
          {
              Id = id;
              OrderStatusId = orderStatusId;
              OrderStatus = orderStatus;
              OrderAmount = orderAmount;
              OrderDate = orderDate;
          } 
    }
}