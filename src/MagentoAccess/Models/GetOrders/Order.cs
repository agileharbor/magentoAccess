﻿using System;
using System.Collections.Generic;
using System.Linq;
using MagentoAccess.MagentoSoapServiceReference;
using MagentoAccess.Misc;
using MagentoAccess.Models.Services.Soap.GetOrders;

namespace MagentoAccess.Models.GetOrders
{
	public class Order : IEquatable< Order >
	{
		public string CustomerEmail { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string ShippingName { get; set; }
		public string ShippingMethod { get; set; }
		public string ShippingLastname { get; set; }
		public string ShippingFirstname { get; set; }
		public string ShippingDescription { get; set; }
		public string ShippingAddressId { get; set; }
		public string OrderId { get; set; }
		public OrderStatusesEnum Status { get; set; }
		public string Customer { get; set; }
		public double BaseDiscount { get; set; }
		public decimal BaseGrandTotal { get; set; }
		public decimal BaseShippingAmount { get; set; }
		public decimal BaseShippingTaxAmount { get; set; }
		public decimal BaseSubtotal { get; set; }
		public decimal BaseTaxAmount { get; set; }
		public decimal BaseTotalPaid { get; set; }
		public decimal BaseTotalRefunded { get; set; }
		public double DiscountAmount { get; set; }
		public decimal GrandTotal { get; set; }
		public decimal ShippingAmount { get; set; }
		public decimal ShippingTaxAmount { get; set; }
		public decimal StoreToOrderRate { get; set; }
		public decimal Subtotal { get; set; }
		public decimal TaxAmount { get; set; }
		public decimal TotalPaid { get; set; }
		public decimal TotalRefunded { get; set; }
		public decimal BaseShippingDiscountAmount { get; set; }
		public decimal BaseSubtotalInclTax { get; set; }
		public decimal BaseTotalDue { get; set; }
		public decimal ShippingDiscountAmount { get; set; }
		public decimal SubtotalInclTax { get; set; }
		public decimal TotalDue { get; set; }
		public string BaseCurrencyCode { get; set; }
		public string StoreName { get; set; }
		public DateTime CreatedAt { get; set; }
		public decimal ShippingInclTax { get; set; }
		public string PaymentMethod { get; set; }
		public List< Tuple< AddressTypeEnum, Address > > Addresses { get; set; }
		public List< Item > Items { get; set; }
		public List< Comment > Comments { get; set; }
		public OrderStateEnum State { get; set; }
		public string OrderIncrementalId { get; set; }

		internal Order( OrderInfoResponse order )
		{
			var billingAddress = Tuple.Create(
				AddressTypeEnum.Billing,
				new Address
				{
					AddressType = order.BillingAddress?.AddressType,
					City = order.BillingAddress?.City,
					Company = order.BillingAddress?.Company,
					CountryId = order.BillingAddress?.CountryId,
					Firstname = order.BillingAddress?.Firstname,
					Lastname = order.BillingAddress?.Lastname,
					Postcode = order.BillingAddress?.Postcode,
					Region = order.BillingAddress?.Region,
					Street = order.BillingAddress?.Street,
					Telephone = order.BillingAddress?.Telephone,
				} );
			var shippingAddress = Tuple.Create(
				AddressTypeEnum.Shipping,
				order.ShippingAddress == null ? new Address() : new Address
				{
					AddressType = order.ShippingAddress.AddressType,
					City = order.ShippingAddress.City,
					Company = order.ShippingAddress.Company,
					CountryId = order.ShippingAddress.CountryId,
					Firstname = order.ShippingAddress.Firstname,
					Lastname = order.ShippingAddress.Lastname,
					Postcode = order.ShippingAddress.Postcode,
					Region = order.ShippingAddress.Region,
					Street = order.ShippingAddress.Street,
					Telephone = order.ShippingAddress.Telephone,
				} );

			this.Addresses = new List< Tuple< AddressTypeEnum, Address > >
			{
				billingAddress,
				shippingAddress
			};

			this.BaseCurrencyCode = order.BaseCurrencyCode;
			this.BaseDiscount = order.BaseDiscountAmount.ToDoubleOrDefault();
			this.BaseGrandTotal = order.BaseGrandTotal.ToDecimalOrDefault();
			this.BaseShippingAmount = order.BaseShippingAmount.ToDecimalOrDefault();
			this.BaseSubtotal = order.BaseSubtotal.ToDecimalOrDefault();
			this.BaseTaxAmount = order.BaseTaxAmount.ToDecimalOrDefault();
			this.BaseTotalPaid = order.BaseTotalPaid.ToDecimalOrDefault();
			this.BaseTotalRefunded = order.BaseTotalRefunded.ToDecimalOrDefault();
			this.CreatedAt = order.CreatedAt.ToDateTimeOrDefault();
			this.Customer = order.CustomerId;
			this.DiscountAmount = order.DiscountAmount.ToDoubleOrDefault();
			this.GrandTotal = order.GrandTotal.ToDecimalOrDefault();
			this.OrderIncrementalId = order.IncrementId;
			this.OrderId = order.OrderId;
			this.Items = order.Items.ToSvOrderItems().ToList();
			this.PaymentMethod = order.Payment?.Method;
			this.StoreName = order.StoreName;
			this.Subtotal = order.Subtotal.ToDecimalOrDefault();
			this.TaxAmount = order.TaxAmount.ToDecimalOrDefault();
			this.TotalPaid = order.TotalPaid.ToDecimalOrDefault();
			this.TotalRefunded = order.TotalRefunded.ToDecimalOrDefault();

			this.ShippingAddressId = order.ShippingAddressId;
			this.ShippingAmount = order.ShippingAmount.ToDecimalOrDefault();

			this.ShippingDescription = order.ShippingDescription;
			this.ShippingFirstname = order.ShippingFirstname;
			this.ShippingLastname = order.ShippingLastname;
			this.ShippingMethod = order.ShippingMethod;
			this.ShippingName = order.ShippingName;
			this.CustomerEmail = order.CustomerEmail;
			this.UpdatedAt = order.UpdatedAT.ToDateTimeOrDefault();
			OrderStatusesEnum tempstatus;
			OrderStateEnum tempstate;
			this.Status = Enum.TryParse( order.Status, true, out tempstatus ) ? tempstatus : OrderStatusesEnum.unknown;
			this.State = Enum.TryParse( order.State, true, out tempstate ) ? tempstate : OrderStateEnum.unknown;
		}

		public Order( salesOrderEntity order )
		{
			this.Addresses = new List< Tuple< AddressTypeEnum, Address > >
			{
				Tuple.Create( AddressTypeEnum.Billing,
					new Address
					{
						AddressType = order.billing_address.address_type,
						City = order.billing_address.city,
						Company = order.billing_address.company,
						CountryId = order.billing_address.country_id,
						Firstname = order.billing_address.firstname,
						Lastname = order.billing_address.lastname,
						Postcode = order.billing_address.postcode,
						Region = order.billing_address.region,
						Street = order.billing_address.street,
						Telephone = order.billing_address.telephone,
					} ),
				Tuple.Create( AddressTypeEnum.Shipping,
					new Address
					{
						AddressType = order.shipping_address.address_type,
						City = order.shipping_address.city,
						Company = order.shipping_address.company,
						CountryId = order.shipping_address.country_id,
						Firstname = order.shipping_address.firstname,
						Lastname = order.shipping_address.lastname,
						Postcode = order.shipping_address.postcode,
						Region = order.shipping_address.region,
						Street = order.shipping_address.street,
						Telephone = order.shipping_address.telephone,
					} ),
			};

			this.BaseCurrencyCode = order.base_currency_code;
			this.BaseDiscount = order.base_discount_amount.ToDoubleOrDefault();
			this.BaseGrandTotal = order.base_grand_total.ToDecimalOrDefault();
			this.BaseShippingAmount = order.base_shipping_amount.ToDecimalOrDefault();
			this.BaseSubtotal = order.base_subtotal.ToDecimalOrDefault();
			this.BaseTaxAmount = order.base_tax_amount.ToDecimalOrDefault();
			this.BaseTotalPaid = order.base_total_paid.ToDecimalOrDefault();
			this.BaseTotalRefunded = order.base_total_refunded.ToDecimalOrDefault();
			this.CreatedAt = order.created_at.ToDateTimeOrDefault();
			this.Customer = order.customer_id;
			this.DiscountAmount = order.discount_amount.ToDoubleOrDefault();
			this.GrandTotal = order.grand_total.ToDecimalOrDefault();
			this.OrderIncrementalId = order.increment_id;
			this.OrderId = order.order_id;
			this.Items = order.items.Select( x => new Item
				{
					BaseDiscountAmount = x.base_discount_amount.ToDecimalOrDefault(),
					BaseOriginalPrice = x.base_original_price.ToDecimalOrDefault(),
					Sku = x.sku,
					Name = x.name,
					BaseTaxAmount = x.base_tax_amount.ToDecimalOrDefault(),
					ItemId = x.item_id,
					BasePrice = x.base_price.ToDecimalOrDefault(),
					BaseRowTotal = x.base_row_total.ToDecimalOrDefault(),
					DiscountAmount = x.discount_amount.ToDecimalOrDefault(),
					OriginalPrice = x.original_price.ToDecimalOrDefault(),
					Price = x.price.ToDecimalOrDefault(),
					ProductType = x.product_type,
					QtyCanceled = x.qty_canceled.ToDecimalOrDefault(),
					QtyInvoiced = x.qty_invoiced.ToDecimalOrDefault(),
					QtyOrdered = x.qty_ordered.ToDecimalOrDefault(),
					QtyShipped = x.qty_shipped.ToDecimalOrDefault(),
					QtyRefunded = x.qty_refunded.ToDecimalOrDefault(),
					RowTotal = x.row_total.ToDecimalOrDefault(),
					TaxAmount = x.tax_amount.ToDecimalOrDefault(),
					TaxPercent = x.tax_percent.ToDecimalOrDefault(),
				}
			).ToList();
			this.PaymentMethod = order.payment.method;
			this.ShippingAmount = order.shipping_amount.ToDecimalOrDefault();
			this.StoreName = order.store_name;
			this.Subtotal = order.subtotal.ToDecimalOrDefault();
			this.TaxAmount = order.tax_amount.ToDecimalOrDefault();
			this.TotalPaid = order.total_paid.ToDecimalOrDefault();
			this.TotalRefunded = order.total_refunded.ToDecimalOrDefault();

			this.ShippingAddressId = order.shipping_address_id;
			this.ShippingAmount = order.shipping_amount.ToDecimalOrDefault();

			this.ShippingDescription = order.shipping_description;
			this.ShippingFirstname = order.shipping_firstname;
			this.ShippingLastname = order.shipping_lastname;
			this.ShippingMethod = order.shipping_method;
			this.ShippingName = order.shipping_name;
			this.CustomerEmail = order.customer_email;

			OrderStatusesEnum tempstatus;
			OrderStateEnum tempstate;
			this.Status = Enum.TryParse( order.status, true, out tempstatus ) ? tempstatus : OrderStatusesEnum.unknown;
			this.State = Enum.TryParse( order.state, true, out tempstate ) ? tempstate : OrderStateEnum.unknown;
		}

		public override bool Equals( object obj )
		{
			if( ReferenceEquals( this, obj ) )
				return true;

			var order = obj as Order;
			if( order == null )
				return false;
			else
				return Equals( order );
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var result = this.CustomerEmail.GetHashCode();
				result = ( result * 381 ) ^ this.UpdatedAt.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingName.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingMethod.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingLastname.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingFirstname.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingDescription.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingAddressId.GetHashCode();
				result = ( result * 381 ) ^ this.OrderId.GetHashCode();
				result = ( result * 381 ) ^ this.Status.GetHashCode();
				result = ( result * 381 ) ^ this.Customer.GetHashCode();
				result = ( result * 381 ) ^ this.BaseDiscount.GetHashCode();
				result = ( result * 381 ) ^ this.BaseGrandTotal.GetHashCode();
				result = ( result * 381 ) ^ this.BaseShippingAmount.GetHashCode();
				result = ( result * 381 ) ^ this.BaseShippingTaxAmount.GetHashCode();
				result = ( result * 381 ) ^ this.BaseSubtotal.GetHashCode();
				result = ( result * 381 ) ^ this.BaseTaxAmount.GetHashCode();
				result = ( result * 381 ) ^ this.BaseTotalPaid.GetHashCode();
				result = ( result * 381 ) ^ this.BaseTotalRefunded.GetHashCode();
				result = ( result * 381 ) ^ this.DiscountAmount.GetHashCode();
				result = ( result * 381 ) ^ this.GrandTotal.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingAmount.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingTaxAmount.GetHashCode();
				result = ( result * 381 ) ^ this.StoreToOrderRate.GetHashCode();
				result = ( result * 381 ) ^ this.Subtotal.GetHashCode();
				result = ( result * 381 ) ^ this.TaxAmount.GetHashCode();
				result = ( result * 381 ) ^ this.TotalPaid.GetHashCode();
				result = ( result * 381 ) ^ this.TotalRefunded.GetHashCode();
				result = ( result * 381 ) ^ this.BaseShippingDiscountAmount.GetHashCode();
				result = ( result * 381 ) ^ this.BaseSubtotalInclTax.GetHashCode();
				result = ( result * 381 ) ^ this.BaseTotalDue.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingDiscountAmount.GetHashCode();
				result = ( result * 381 ) ^ this.SubtotalInclTax.GetHashCode();
				result = ( result * 381 ) ^ this.TotalDue.GetHashCode();
				result = ( result * 381 ) ^ this.BaseCurrencyCode.GetHashCode();
				result = ( result * 381 ) ^ this.StoreName.GetHashCode();
				result = ( result * 381 ) ^ this.CreatedAt.GetHashCode();
				result = ( result * 381 ) ^ this.ShippingInclTax.GetHashCode();
				result = ( result * 381 ) ^ this.PaymentMethod.GetHashCode();
				result = ( result * 381 ) ^ this.Addresses.GetHashCode();
				result = ( result * 381 ) ^ this.Items.GetHashCode();
				result = ( result * 381 ) ^ this.Comments.GetHashCode();
				result = ( result * 381 ) ^ this.State.GetHashCode();
				result = ( result * 381 ) ^ this.OrderIncrementalId.GetHashCode();
				return result;
			}
		}

		public bool Equals( Order order )
		{
			if( order == null )
				return false;

			var result = Equals( this.CustomerEmail, order.CustomerEmail ) &&
			             Equals( this.UpdatedAt, order.UpdatedAt ) &&
			             Equals( this.ShippingName, order.ShippingName ) &&
			             Equals( this.ShippingMethod, order.ShippingMethod ) &&
			             Equals( this.ShippingLastname, order.ShippingLastname ) &&
			             Equals( this.ShippingFirstname, order.ShippingFirstname ) &&
			             Equals( this.ShippingDescription, order.ShippingDescription ) &&
			             Equals( this.ShippingAddressId, order.ShippingAddressId ) &&
			             Equals( this.OrderId, order.OrderId ) &&
			             Equals( this.Status, order.Status ) &&
			             Equals( this.Customer, order.Customer ) &&
			             Equals( this.BaseDiscount, order.BaseDiscount ) &&
			             Equals( this.BaseGrandTotal, order.BaseGrandTotal ) &&
			             Equals( this.BaseShippingAmount, order.BaseShippingAmount ) &&
			             Equals( this.BaseShippingTaxAmount, order.BaseShippingTaxAmount ) &&
			             Equals( this.BaseSubtotal, order.BaseSubtotal ) &&
			             Equals( this.BaseTaxAmount, order.BaseTaxAmount ) &&
			             Equals( this.BaseTotalPaid, order.BaseTotalPaid ) &&
			             Equals( this.BaseTotalRefunded, order.BaseTotalRefunded ) &&
			             Equals( this.DiscountAmount, order.DiscountAmount ) &&
			             Equals( this.GrandTotal, order.GrandTotal ) &&
			             Equals( this.ShippingAmount, order.ShippingAmount ) &&
			             Equals( this.ShippingTaxAmount, order.ShippingTaxAmount ) &&
			             Equals( this.StoreToOrderRate, order.StoreToOrderRate ) &&
			             Equals( this.Subtotal, order.Subtotal ) &&
			             Equals( this.TaxAmount, order.TaxAmount ) &&
			             Equals( this.TotalPaid, order.TotalPaid ) &&
			             Equals( this.TotalRefunded, order.TotalRefunded ) &&
			             Equals( this.BaseShippingDiscountAmount, order.BaseShippingDiscountAmount ) &&
			             Equals( this.BaseSubtotalInclTax, order.BaseSubtotalInclTax ) &&
			             Equals( this.BaseTotalDue, order.BaseTotalDue ) &&
			             Equals( this.ShippingDiscountAmount, order.ShippingDiscountAmount ) &&
			             Equals( this.SubtotalInclTax, order.SubtotalInclTax ) &&
			             Equals( this.TotalDue, order.TotalDue ) &&
			             Equals( this.BaseCurrencyCode, order.BaseCurrencyCode ) &&
			             Equals( this.StoreName, order.StoreName ) &&
			             Equals( this.CreatedAt, order.CreatedAt ) &&
			             Equals( this.ShippingInclTax, order.ShippingInclTax ) &&
			             Equals( this.PaymentMethod, order.PaymentMethod ) &&
			             this.Addresses.Equal( order.Addresses ) &&
						 this.Items.Equal( order.Items ) &&
			             Equals( this.Comments, order.Comments ) &&
			             Equals( this.State, order.State ) &&
			             Equals( this.OrderIncrementalId, order.OrderIncrementalId );
			return result;
		}
	}

	public enum OrderStateEnum
	{
		unknown,
		@new,
		pending_payment,
		processing,
		complete,
		closed,
		canceled,
		holded
	}

	public enum AddressTypeEnum
	{
		Unknown = 0,
		Billing = 1,
		Shipping = 2,
		Other = 3
	}

	public class OrderId
	{
		public OrderId( string id, bool isIncremental )
		{
			this.Id = id;
			this.IsIncrementalId = isIncremental;
		}

		public string Id { get; private set; }

		public bool IsIncrementalId { get; private set; }
	}

	public static class OrderExtensions
	{
		public static bool IsShipped( this Order order )
		{
			return order.Items.ToList().TrueForAll( x => x.IsShipped() );
		}

		public static OrderId GetId( this Order order )
		{
			return !string.IsNullOrWhiteSpace( order.OrderIncrementalId ) ? new OrderId( order.OrderIncrementalId, true ) : new OrderId( order.OrderId, false );
		}

		public static bool Equal( this List< Tuple< AddressTypeEnum, Address > > o1, List< Tuple< AddressTypeEnum, Address > > o2 )
		{
			if( ReferenceEquals( o1, o2 ) )
				return true;

			if( o1 == null && o2 == null )
				return true;

			if( o1.Count != o2.Count )
				return false;

			var o1Sorted = o1.OrderBy( x => x.Item1 ).ToList();
			var o2Sorted = o2.OrderBy( x => x.Item1 ).ToList();

			var conntinueFlag = true;
			for( var i = 0; i < o1.Count && conntinueFlag; i++ )
			{
				conntinueFlag &= o2Sorted[ i ].Item1.Equals( o1Sorted[ i ].Item1 );
				conntinueFlag &= o2Sorted[ i ].Item2.Equals( o1Sorted[ i ].Item2 );
			}

			return conntinueFlag;
		}

		public static bool Equal( this List< Item > o1, List< Item > o2 )
		{
			if( ReferenceEquals( o1, o2 ) )
				return true;

			if( o1 == null && o2 == null )
				return true;

			if( o1.Count != o2.Count )
				return false;

			var o1Sorted = o1.OrderBy( x => x.ItemId ).ThenBy( y => y.Name ).ThenBy( z => z.Price ).ToList();
			var o2Sorted = o2.OrderBy( x => x.ItemId ).ThenBy( y => y.Name ).ThenBy( z => z.Price ).ToList();

			var conntinueFlag = true;
			conntinueFlag = o1Sorted.SequenceEqual( o2Sorted );
			return conntinueFlag;
		}

		internal static IEnumerable< Item > ToSvOrderItems( this IEnumerable< OrderItemEntity > items )
		{
			if ( items == null )
				return new List< Item >();
			return items.Select( x => new Item
				{
					BaseDiscountAmount = x.BaseDiscountAmount.ToDecimalOrDefault(),
					BaseOriginalPrice = x.BaseOriginalPrice.ToDecimalOrDefault(),
					Sku = x.Sku,
					Name = x.Name,
					BaseTaxAmount = x.BaseTaxAmount.ToDecimalOrDefault(),
					ItemId = x.ItemId,
					BasePrice = x.BasePrice.ToDecimalOrDefault(),
					BaseRowTotal = x.BaseRowTotal.ToDecimalOrDefault(),
					DiscountAmount = x.DiscountAmount.ToDecimalOrDefault(),
					OriginalPrice = x.OriginalPrice.ToDecimalOrDefault(),
					Price = x.Price.ToDecimalOrDefault(),
					ProductType = x.ProductType,
					QtyCanceled = x.QtyCanceled.ToDecimalOrDefault(),
					QtyInvoiced = x.QtyInvoiced.ToDecimalOrDefault(),
					QtyOrdered = x.QtyOrdered.ToDecimalOrDefault(),
					QtyShipped = x.QtyShipped.ToDecimalOrDefault(),
					QtyRefunded = x.QtyRefunded.ToDecimalOrDefault(),
					RowTotal = x.RowTotal.ToDecimalOrDefault(),
					TaxAmount = x.TaxAmount.ToDecimalOrDefault(),
					TaxPercent = x.TaxPercent.ToDecimalOrDefault()
				}
			);
		}
	}
}