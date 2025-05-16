using Google.Protobuf.WellKnownTypes;
using InvoiceServiceProvider;
using Presentation.Factories;
using Presentation.Models.Invoices;

namespace Tests.PresentationTests;

public class InvoiceFactory_Tests
{
    [Fact]
    public void ToInvoiceViewModel_ShouldReturnNull_WithNullInput()
    {
        // Act
        var result = InvoiceFactory.ToInvoiceViewModel(null!);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToInvoiceViewModel_ShouldReturnInvoiceModel_MapsAllProperties()
    {
        //GPT helped me with all mapping asserts and the specifykind part for setting up the invoice in arrange.
        // Arrange
        var invoice = new Invoice
        {
            InvoiceId = "inv123",
            BookingId = "book456",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Address = "123 Main St",
            PostalCode = "54321",
            City = "TestCity",
            EventName = "Concert Night",
            EventDate = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2025, 5, 16), DateTimeKind.Utc)),
            TicketAmount = 2,
            TicketPrice = 49.99,
            TotalPrice = 99.98,
            BookingDate = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2025, 5, 1), DateTimeKind.Utc)),
            CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2025, 5, 2), DateTimeKind.Utc)),
            DueDate = Timestamp.FromDateTime(DateTime.SpecifyKind(new DateTime(2025, 6, 1), DateTimeKind.Utc)),
            Paid = true,
            Deleted = false
        };
        
        

        // Act
        var vm = InvoiceFactory.ToInvoiceViewModel(invoice);

        // Assert
        Assert.NotNull(vm);
        Assert.Equal(invoice.InvoiceId, vm.InvoiceId);
        Assert.Equal(invoice.BookingId, vm.BookingId);
        Assert.Equal(invoice.FirstName, vm.FirstName);
        Assert.Equal(invoice.LastName, vm.LastName);
        Assert.Equal(invoice.PhoneNumber, vm.PhoneNumber);
        Assert.Equal(invoice.Address, vm.Address);
        Assert.Equal(invoice.PostalCode, vm.PostalCode);
        Assert.Equal(invoice.City, vm.City);
        Assert.Equal(invoice.EventName, vm.EventName);
        Assert.Equal(invoice.EventDate.ToDateTime(), TimestampFactory.FromViewModel(vm.EventDate).ToDateTime());
        Assert.Equal(invoice.TicketAmount, vm.TicketAmount);
        Assert.Equal(invoice.TicketPrice, vm.TicketPrice);
        Assert.Equal(invoice.TotalPrice, vm.TotalPrice);
        Assert.Equal(invoice.BookingDate.ToDateTime(), TimestampFactory.FromViewModel(vm.BookingDate).ToDateTime());
        Assert.Equal(invoice.CreatedDate.ToDateTime(), TimestampFactory.FromViewModel(vm.CreatedDate).ToDateTime());
        Assert.Equal(invoice.DueDate.ToDateTime(), TimestampFactory.FromViewModel(vm.DueDate).ToDateTime());
        Assert.Equal(invoice.Paid, vm.Paid);
        Assert.Equal(invoice.Deleted, vm.Deleted);
    }

    [Fact]
    public void ToInvoice_ShouldReturnNull_WithNullInput()
    {
        // Act
        var result = InvoiceFactory.ToInvoice(null!);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToInvoice_ShouldReturnInvoice_MapsAllProperties()
    {
        //Arrange
        var viewModel = new InvoiceViewModel
        {
            InvoiceId   = "INV001",
            BookingId   = "BK001",
            FirstName   = "Alice",
            LastName    = "Smith",
            PhoneNumber = "+46701234567",
            Address     = "Storgatan 1",
            PostalCode  = "111 22",
            City        = "Stockholm",
            EventName   = "Concert",
            EventDate   = "2025-06-15",
            BookingDate = "2025-05-01",
            CreatedDate = "2025-05-02",
            DueDate     = "2025-06-01",
            TicketAmount= 2,
            TicketPrice = 250.00,
            TotalPrice  = 500.00,
            Paid        = false,
            Deleted     = false
        };

        // Act
        var invoice = InvoiceFactory.ToInvoice(viewModel);
        
        //Assert
        Assert.NotNull(invoice);
        Assert.Equal(viewModel.InvoiceId, invoice.InvoiceId);
        Assert.Equal(viewModel.BookingId, invoice.BookingId);
        Assert.Equal(viewModel.FirstName, invoice.FirstName);
        Assert.Equal(viewModel.LastName, invoice.LastName);
        Assert.Equal(viewModel.PhoneNumber, invoice.PhoneNumber);
        Assert.Equal(viewModel.Address, invoice.Address);
        Assert.Equal(viewModel.PostalCode, invoice.PostalCode);
        Assert.Equal(viewModel.City, invoice.City);
        Assert.Equal(viewModel.EventName, invoice.EventName);
        Assert.Equal(viewModel.EventDate, TimestampFactory.ToViewModel(invoice.EventDate));
        Assert.Equal(viewModel.BookingDate, TimestampFactory.ToViewModel(invoice.BookingDate));
        Assert.Equal(viewModel.CreatedDate, TimestampFactory.ToViewModel(invoice.CreatedDate));
        Assert.Equal(viewModel.DueDate, TimestampFactory.ToViewModel(invoice.DueDate));
        Assert.Equal(viewModel.TicketAmount, invoice.TicketAmount);
        Assert.Equal(viewModel.TicketPrice, invoice.TicketPrice);
        Assert.Equal(viewModel.TotalPrice, invoice.TotalPrice);
        Assert.Equal(viewModel.Paid, invoice.Paid);
        Assert.Equal(viewModel.Deleted, invoice.Deleted);
    }
}