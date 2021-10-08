using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using Core.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TicketTests
{
    [TestFixture]
    public class BuyTicketTest
    {
        ITicketService ticketService;
        Mock<IAccountRepository> accountRepository;
        Mock<IScreeningRepository> screeningRepository;
        Mock<ITicketRepository> ticketRepository;


        [SetUp]
        public void Setup()
        {
            accountRepository = new Mock<IAccountRepository>();
            screeningRepository = new Mock<IScreeningRepository>();
            ticketRepository = new Mock<ITicketRepository>();
            ticketService = new TicketService(accountRepository.Object, screeningRepository.Object, ticketRepository.Object);
        }
        
        [Test]
        public void BuyTicketTest_ScreeningDoesNotExist_ThrowsArgumentException()
        {
            screeningRepository.Setup(x => x.GetScreeningByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Screening>(null));
            BuyTicketDto ticketDto = new BuyTicketDto
            {
                UserId = 1,
                ScreeningId = 1,
                NumberOfTickets = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => ticketService.BuyTicket(ticketDto));
            Assert.That(ex.Message, Is.EqualTo("Screening with given Id does not exist"));
        }

        [Test]
        public void BuyTicketTest_UserDoesNotExist_ThrowsArgumentException()
        {
            screeningRepository.Setup(x => x.GetScreeningByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Screening>(new Screening { }));
            accountRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<User>(null));

            BuyTicketDto ticketDto = new BuyTicketDto
            {
                UserId = 1,
                ScreeningId = 1,
                NumberOfTickets = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => ticketService.BuyTicket(ticketDto));
            Assert.That(ex.Message, Is.EqualTo("User with given id does not exist"));
        }

        [Test]
        [TestCase(10, 1)]
        [TestCase(5, 6)]
        [TestCase(3, 8)]
        [TestCase(0, 11)]
        public void BuyTicketTest_UserBuysMoreThan10Tickets_ThrowsArgumentException(int alreadyBought, int toBeBought)
        {
            screeningRepository.Setup(x => x.GetScreeningByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Screening>(new Screening { }));
            accountRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<User>(new User { }));
            ticketRepository.Setup(x => x.GetNumberOfTicketsAlreadyBought(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(alreadyBought));

            BuyTicketDto ticketDto = new BuyTicketDto
            {
                UserId = 1,
                ScreeningId = 1,
                NumberOfTickets = toBeBought
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => ticketService.BuyTicket(ticketDto));
            Assert.That(ex.Message, Is.EqualTo("Unable to buy more than 10 tickets"));
        }

        [Test]
        [TestCase(9, 1)]
        [TestCase(5, 3)]
        [TestCase(3, 7)]
        [TestCase(0, 10)]
        public void BuyTicketTest_UserBuys10OrLessTickets_ticketRepositoryBuyTicketGetsInvoked(int alreadyBought, int toBeBought)
        {
            screeningRepository.Setup(x => x.GetScreeningByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Screening>(new Screening { TicketPrice = 10 }));
            accountRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<User>(new User { }));
            ticketRepository.Setup(x => x.GetNumberOfTicketsAlreadyBought(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(alreadyBought));
            ticketRepository.Setup(x => x.BuyTicket(It.IsAny<Ticket>())).Returns(Task.FromResult(true));

            BuyTicketDto ticketDto = new BuyTicketDto
            {
                UserId = 1,
                ScreeningId = 1,
                NumberOfTickets = toBeBought
            };

            ticketService.BuyTicket(ticketDto);

            ticketRepository.Verify(x => x.BuyTicket(It.IsAny<Ticket>()), Times.Exactly(toBeBought));
        }


    }
}
